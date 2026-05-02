using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Identity.Services;
using ClothingStore.Infrastructure.Logging;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Persistence.Repositories;
using ClothingStore.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        // Identity
        services.AddIdentity<ApplicationUser, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();



        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(SerilogAppLogger<>));


        return services;
    }
}