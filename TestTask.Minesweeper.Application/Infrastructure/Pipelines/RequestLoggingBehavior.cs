
using MediatR;

using Microsoft.Extensions.Logging;

namespace TestTask.Minesweeper.Application.Infrastructure.Pipelines
{
	/// <summary>
	/// Represents a <see cref="IPipelineBehavior{TRequest, TResponse}"/> for logging requests.
	/// </summary>
	/// <typeparam name="TRequest">Type of request.</typeparam>
	/// <typeparam name="TResponse">Type of response.</typeparam>
	internal sealed class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogger _logger;
		private readonly string _requestTypeName;

		/// <summary>
		/// Initializes a new instance of <see cref="RequestLoggingBehavior{TRequest, TResponse}"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for <see cref="RequestLoggingBehavior{TRequest, TResponse}"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		public RequestLoggingBehavior(ILogger<ApplicationFaultExceptionProcessorBehavior<TRequest, TResponse>> logger)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_requestTypeName = typeof(TRequest).FullName ?? "Unknown";
		}

		/// <inheritdoc/>
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			_logger.LogDebug(@"Start processing of request ""{name}"".", _requestTypeName);

			try
			{
				var response = await next(cancellationToken)
										.ConfigureAwait(false);

				_logger.LogDebug(@"Processing of request ""{name}"" was completed successfully.", _requestTypeName);

				return response;
			}
			catch (OperationCanceledException)
			{
				_logger.LogDebug(@"Processing of request ""{name}"" was failed.", _requestTypeName);

				throw;
			}
			catch (Exception)
			{
				_logger.LogError(@"Processing of request ""{name}"" was failed.", _requestTypeName);

				throw;
			}
		}
	}
}
