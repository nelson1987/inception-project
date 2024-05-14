using FluentValidation;
using InceptionClean.Application.Extensions;
using InceptionClean.Application.Features.Persons.Create;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InceptionClean.Application.Behaviors;
public sealed class MyPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Pre-processing logic

        var response = await next();

        // Post-processing logic

        return response;
    }
}

public sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new();
        _logger.LogInformation($"Handling: {typeof(TRequest).Name} | Json:\n{request.ToJson()}");
        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        _logger.LogInformation($"Handled {typeof(TResponse).Name} in {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Reset();

        return response;
    }
}

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        /*
         
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return UnprocessableEntity(validationResult.ToModelState());
         */
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context,cancellationToken)));

        var errors = _validators
                 .Select(x => x.Validate(context))
                 .SelectMany(x => x.Errors)
                 .Where(x => x != null);

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}