using MediatR;
using ClothingStore.Application.Interfaces.Services;

namespace ClothingStore.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{

    private readonly IAppLogger<TRequest> _logger;

    public LoggingBehavior(IAppLogger<TRequest> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {Request}", typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation("Handled {Request}", typeof(TRequest).Name);

        return response;
    }
}