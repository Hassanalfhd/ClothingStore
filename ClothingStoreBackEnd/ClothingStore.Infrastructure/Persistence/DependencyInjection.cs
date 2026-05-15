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


        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepo, CategoryRepo>();
        services.AddScoped<IBrandRepo, BrandRepo>();
        services.AddScoped<ISizeRepo, SizeRepo>();
        services.AddScoped<IColorRepo, ColorRepo>();
        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<IProductReadRepos, ProductReadRepo>();
        services.AddScoped<IProductVariantRepo, ProductVariantRepo>();

        return services;
    }
}