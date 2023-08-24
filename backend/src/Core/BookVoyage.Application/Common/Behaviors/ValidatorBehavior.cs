using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookVoyage.Application.Common.Behaviors;

public class ValidatorBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                foreach (var failure in failures)
                {
                    _logger.LogError($"Validation failure: Property '{failure.PropertyName}' Error: '{failure.ErrorMessage}'");
                }
            }
            throw new FluentValidation.ValidationException(failures);
        }
        return await next();
    }
}