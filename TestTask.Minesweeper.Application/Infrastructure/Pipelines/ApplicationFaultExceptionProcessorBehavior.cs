using MediatR;

using Microsoft.Extensions.Logging;

using TestTask.Minesweeper.Application.Exceptions;

namespace TestTask.Minesweeper.Application.Infrastructure.Pipelines
{
	/// <summary>
	/// Represents a processor of application's exceptions.
	/// </summary>
	/// <typeparam name="TRequest">Type of request.</typeparam>
	/// <typeparam name="TResponse">Type of response.</typeparam>
	internal sealed class ApplicationFaultExceptionProcessorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogger _logger;

		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationFaultExceptionProcessorBehavior{TRequest, TResponse}"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for <see cref="ApplicationFaultExceptionProcessorBehavior{TRequest, TResponse}"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		public ApplicationFaultExceptionProcessorBehavior(ILogger<ApplicationFaultExceptionProcessorBehavior<TRequest, TResponse>> logger)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc/>
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			try
			{
				return await next(cancellationToken)
								.ConfigureAwait(false);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (ApplicationFaultException)
			{
				throw;
			}
			catch (Exception exception)
			{
				throw new ApplicationFaultCommonException(exception);
			}
		}
	}
}
