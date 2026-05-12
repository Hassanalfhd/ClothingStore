using MediatR;
using Microsoft.Extensions.Logging;

namespace ClothingStore.Application.Common.Behaviors;
public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var start = DateTime.UtcNow;

        _logger.LogInformation("Handling {Request}", requestName);

        var response = await next();

        var duration = DateTime.UtcNow - start;

        _logger.LogInformation(
            "Handled {Request} in {Duration} ms",
            requestName,
            duration.TotalMilliseconds);

        return response;
    }
}