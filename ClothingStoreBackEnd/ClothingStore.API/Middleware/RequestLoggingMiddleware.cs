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

        await _next(context);

        var duration = DateTime.UtcNow - start;

        var statusCode = context.Response.StatusCode;

        _logger.LogInformation(
            "HTTP {Method} {Path} responded {StatusCode} in {Duration} ms",
            context.Request.Method,
            context.Request.Path,
            statusCode,
            duration.TotalMilliseconds);


        if (duration.TotalMilliseconds > 1000)
        {
            _logger.LogWarning(
                "Slow request: {Path} took {Duration} ms",
                context.Request.Path,
                duration.TotalMilliseconds);
        }
    }
}

