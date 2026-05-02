using ClothingStore.Application.Common.Behaviors;
using ClothingStore.Application.Features.Catalog.Category;
using ClothingStore.Application.Interfaces.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 1. CQRS (Handlers)
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // 2. Validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // 3. Pipeline Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        //4. Services
        services.AddScoped<ICategoryService, CategoryService>();


        return services;
    }
}