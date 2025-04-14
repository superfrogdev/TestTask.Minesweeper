using System.Net;
using System.Text.Json;

namespace TestTask.Minesweeper.Service.Middlewares
{
	/// <summary>
	/// Represents a middleware, which processes processable exceptions as api is requires.
	/// </summary>
	internal sealed class ProcessableExceptionsMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ProcessableExceptionsMiddleware> _logger;
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		/// <summary>
		/// Initializes a new instance of <see cref="ProcessableExceptionsMiddleware"/>.
		/// </summary>
		public ProcessableExceptionsMiddleware(RequestDelegate next, JsonSerializerOptions jsonSerializerOptions, ILogger<ProcessableExceptionsMiddleware> logger)
			: base()
		{
			_next = next;

			_jsonSerializerOptions = jsonSerializerOptions;

			_logger = logger;
		}

		/// <summary>
		/// Processes processable exceptions as api requires.
		/// </summary>
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception exception)
				when (exception is Application.Exceptions.IBusinessException)
			{
				httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				var errorDescription = new Api.ErrorDescription() { Error = exception.Message };

				await httpContext.Response.WriteAsJsonAsync<Api.ErrorDescription>(errorDescription, _jsonSerializerOptions, "application/json", httpContext.RequestAborted);
			}
		}
	}
}
