using System.Text;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Persistence.Repositories;
using ClothingStore.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.Configure<JwtSettings>(
              config.GetSection("Jwt"));


        services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    var jwt = config
                        .GetSection("Jwt")
                        .Get<JwtSettings>()!;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwt.Issuer,
                            ValidAudience = jwt.Audience,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwt.SecretKey))
                        };
                });


        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}