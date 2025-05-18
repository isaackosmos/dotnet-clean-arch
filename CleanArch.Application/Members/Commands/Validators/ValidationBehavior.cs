using FluentValidation;
using MediatR;

namespace CleanArch.Application.Members.Commands.Validators;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(result => result.Errors).Where(failure => failure != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new FluentValidation.ValidationException(failures);
            }
        }

        return await next();
    }
}