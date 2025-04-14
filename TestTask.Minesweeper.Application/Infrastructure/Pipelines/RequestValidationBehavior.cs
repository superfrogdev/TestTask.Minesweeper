using FluentValidation;
using FluentValidation.Results;

using MediatR;

using TestTask.Minesweeper.Application.Exceptions;

namespace TestTask.Minesweeper.Application.Infrastructure.Pipelines
{
	/// <summary>
	/// Represents a <see cref="IPipelineBehavior{TRequest, TResponse}"/>, which validates data of <typeparamref name="TRequest"/>.
	/// </summary>
	/// <typeparam name="TRequest">Type of request.</typeparam>
	/// <typeparam name="TResponse">Type of response.</typeparam>
	internal sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		/// <summary>
		/// Initializes a new instance of <see cref="RequestValidationBehavior{TRequest, TResponse}"/>.
		/// </summary>
		/// <param name="validators">Enumeration of <see cref="IValidator{T}"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="validators"/> cannot be <see langword="null"/>.</exception>
		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
			: base()
		{
			_validators = validators ?? throw new ArgumentNullException(nameof(validators));
		}

		/// <inheritdoc/>
		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			
			var totalFailures = new List<ValidationFailure>();

			foreach (var currentValidator in _validators)
			{
				var result = currentValidator.Validate(context);

				if (!result.IsValid)
				{
					totalFailures.AddRange(result.Errors);
				}
			}
			
			if (totalFailures.Count > 0)
			{
				throw new ValidationFaultException(totalFailures);
			}

			return next(cancellationToken);
		}
	}
}
