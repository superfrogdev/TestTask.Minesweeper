using MediatR;

using Microsoft.Extensions.Logging;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a handler of <see cref="Command"/>.
	/// </summary>
	internal sealed class Handler : IRequestHandler<Command, Result>
	{
		private readonly ILogger _logger;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		public Handler(ILogger<Handler> logger)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc/>
		public Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var result = new Result()
			{
				GameId = Guid.NewGuid(),
				Field = new byte[request.Height, request.Width]
			};

			return Task.FromResult(result);
		}
	}
}
