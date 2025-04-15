using MediatR;

using Microsoft.Extensions.Logging;

namespace TestTask.Minesweeper.Application.Commands.MakeTurn
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
				Field = new Domain.Values.Cell[1, 1],
				FieldSize = new Domain.Values.Size2d(1),
				TurnResult = Domain.Enums.TurnResult.Defeat,
				MinesCount = 0
			};

			return Task.FromResult(result);
		}
	}
}
