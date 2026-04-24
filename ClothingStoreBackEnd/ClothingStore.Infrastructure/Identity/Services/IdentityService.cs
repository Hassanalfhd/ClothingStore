using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace ClothingStore.Infrastructure.Identity.Services
{
    public class IdentityService: IIdentityService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger<IdentityService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepo _userRepo;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, 
            IAppLogger<IdentityService> logger, 
            IUnitOfWork unitOfWork,
            IUserRepo userRepo, 
            ITokenService tokenService,
            IOptions<JwtSettings> jwtSettings,
            IRefreshTokenRepo refreshTokenRepo)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
            _refreshTokenRepo = refreshTokenRepo;
        }
        public async Task<Result<Guid>> RegisterAsync(string Email, string Password, string FirstName, string LastName, CancellationToken cancellationToken = default)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser(Email, FirstName, LastName);
                var result = await _userManager.CreateAsync(user, Password);


                if (!result.Succeeded)
                {

                    return Result<Guid>.Failure(result.Errors.First()?.Description ?? "unexcepted error occurred");
                }

                //user.AddDomainEvent(new UserRegisteredEvent(user.Id, Email, FirstName, LastName));

                var contactInfo = new ContactInfo(Email, null, null);

                var userProfile = new ClothingStore.Domain.Entities.UserProfile(
                    user.Id,
                    contactInfo,
                    FirstName,
                    LastName,
                birthDate: null,
                    profileImage: null

                );


                _userRepo.Add(userProfile);



                _logger.LogInformation("User Registered Successfully", user.PublicId);

                await _userManager.AddToRoleAsync(user, "Customer");

                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();

                return Result<Guid>.Success(user.PublicId);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                 _logger.LogError(ex, "unexcepted error occurred.");
                return Result<Guid>.Failure("unexcepted error occurred.");
            }

        }



        public async Task<Result<AuthResponseDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return Result<AuthResponseDto>.Failure("Invalid credentials.");

            var validPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!validPassword) return Result<AuthResponseDto>.Failure("Invalid credentials.");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles is null) return Result<AuthResponseDto>.Failure("Unexpected error, please contact the support team.");

            var accessToken = await _tokenService.CreateAccessTokenAsync(new TokenRequestDto
            {
                Email = email,
                publicId = user.PublicId.ToString(),
                Roles = roles.ToList()!
            });


            var refreshTokenValue = _tokenService.CreateRefreshToken();


            var refreshToken = new RefreshToken(user.Id, refreshTokenValue, _jwtSettings.RefreshTokenDays);


            _refreshTokenRepo.Add(refreshToken);

            try
            {
                await _unitOfWork.SaveChangesAsync();

                return Result<AuthResponseDto>.Success(new AuthResponseDto
               (
                  accessToken,
                  refreshToken.Token
                ));
            }catch(Exception ex)
            {
                return Result<AuthResponseDto>.Failure("Unexpected error, please contact the support team.");
            }

        }


        public async Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken, string AccessToken, CancellationToken cancellationToken = default)
        {

            var storedToken = await _refreshTokenRepo.GetRefreshTokenByTokenAsync(refreshToken, cancellationToken);

            if(storedToken == null)return Result<AuthResponseDto>.Failure("Invalid refresh token.");

            if(!storedToken.IsActive) return Result<AuthResponseDto>.Failure("Refresh token is expired or revoked.");

            storedToken.MarkAsUsed();
            
            var tokenClaims = _tokenService.GetTokenRequestDtoFromToken(AccessToken);

            var newAccessToken = await _tokenService.CreateAccessTokenAsync(tokenClaims);

            var newRefreshTokenValue = _tokenService.CreateRefreshToken();

            var newRefreshToken = new RefreshToken(storedToken.ApplicationUserId, newRefreshTokenValue, _jwtSettings.RefreshTokenDays);

            _refreshTokenRepo.Add(newRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return Result<AuthResponseDto>.Success(new AuthResponseDto(newAccessToken, newRefreshToken.Token));

        }
    
        public async Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var storedToken = await _refreshTokenRepo.GetRefreshTokenByTokenAsync(refreshToken, cancellationToken);

            if (storedToken == null) return Result.Failure("Token is not valid.");

            if (!storedToken.IsActive) return Result.Failure("Token is not valid.");

            storedToken.Revoke();
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }


    }
}
