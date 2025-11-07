using FluentValidation;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Common
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));

                var failures = results.Where(n => n.Errors.Any()).SelectMany(n => n.Errors).ToList();

                if(failures.Any())
                {
                    throw new Exception("Validation exceptions: " + string.Join(", ", failures.Select(n => n.ErrorMessage)));
                }
            }
            return await next();
        }
    }
}
