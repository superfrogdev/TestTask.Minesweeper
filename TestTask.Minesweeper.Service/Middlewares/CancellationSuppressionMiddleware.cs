namespace TestTask.Minesweeper.Service.Middlewares
{
	/// <summary>
	/// Represents a middleware, which suppress all exceptions for cancelled requests.
	/// </summary>
	internal sealed class CancellationSuppressionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CancellationSuppressionMiddleware> _logger;

		/// <summary>
		/// Initializes a new instance of <see cref="CancellationSuppressionMiddleware"/>.
		/// </summary>
		public CancellationSuppressionMiddleware(RequestDelegate next, ILogger<CancellationSuppressionMiddleware> logger)
			: base()
		{
			_next = next;

			_logger = logger;
		}

		/// <summary>
		/// Suppresses any <see cref="Exception"/> for cancelled request.
		/// </summary>
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception exception)
				when (httpContext.RequestAborted.IsCancellationRequested)
			{
				_logger.LogWarning(exception, "Request was aborted.");
			}
		}
	}
}
