using ClothingStore.Application.Interfaces.Services;

namespace ClothingStore.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAppLogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error");
        }
    }
}