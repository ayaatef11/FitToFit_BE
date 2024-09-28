using FluentValidation;
using MediatR;
using System;

namespace SharedKernal.MediatR.Piplines
{
    //This class implements the IPipelineBehavior<TRequest, TResponse> interface provided by MediatR.
    //A pipeline behavior in MediatR allows you to add pre- and post-processing
    //logic around the execution of request handlers, similar to middleware in ASP.NET Core.
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		// is a middleware component within the MediatR pipeline check
        // if the incoming request is valid before passing it to the next handler in the pipeline
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
			//Task.WhenAll is a static method that takes an IEnumerable<Task> and returns a single task that completes when all of the provided tasks have completed.


			var failures =
                validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);

            return await next();//delegate for the next action in the pipeline ,represnet the handler
        }
    }
}
