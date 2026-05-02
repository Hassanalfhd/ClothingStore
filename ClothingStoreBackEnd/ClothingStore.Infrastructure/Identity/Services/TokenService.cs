using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async  Task<string> CreateAccessTokenAsync(TokenRequestDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.publicId),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim("uid", user.publicId)
        };

        // Roles
        if (user.Roles != null)
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        // Key
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenMinutes),
            signingCredentials: creds
        );

         return new  JwtSecurityTokenHandler().WriteToken(token);
    }
    public string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }


    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, 
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            ValidateLifetime = false 
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public TokenRequestDto GetTokenRequestDtoFromToken(string token)
    {
        var principal = GetPrincipalFromExpiredToken(token);

        var publicId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? principal.FindFirst("sub")?.Value;

        var email = principal.FindFirst(ClaimTypes.Email)?.Value
                    ?? principal.FindFirst("email")?.Value;

        var roles = principal.FindAll(ClaimTypes.Role)
                             .Select(c => c.Value)
                             .ToList();

        return new TokenRequestDto
        {
            publicId = publicId ?? string.Empty,
            Email = email ?? string.Empty,
            Roles = roles
        };
    }

}

