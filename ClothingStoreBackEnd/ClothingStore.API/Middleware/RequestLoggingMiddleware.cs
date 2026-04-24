using ClothingStore.Application.Interfaces.Services;


namespace ClothingStore.API.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAppLogger<RequestLoggingMiddleware> _logger;
    
    public RequestLoggingMiddleware(RequestDelegate next, IAppLogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.UtcNow;

        _logger.LogInformation("Incoming request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);
        
        await _next(context);

        var duration = DateTime.UtcNow - start;


        if (duration.TotalSeconds < 1)
            _logger.LogWarning("Request is slow. it completed in {Duration} ms", duration.TotalMilliseconds);


        _logger.LogInformation("Request completed in {Duration} ms", duration.TotalMilliseconds);

    }
}

