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
		private readonly Domain.Processors.IGameFieldGenerator _gameFieldGenerator;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <param name="gameFieldGenerator">Instance of <see cref="Domain.Processors.IGameFieldGenerator"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameFieldGenerator"/> cannot be <see langword="null"/>.</exception>
		public Handler(ILogger<Handler> logger, Domain.Processors.IGameFieldGenerator gameFieldGenerator)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_gameFieldGenerator = gameFieldGenerator ?? throw new ArgumentNullException(nameof(gameFieldGenerator));
		}

		/// <inheritdoc/>
		public Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var gameField = new Domain.Values.GameField(request.FieldSize);

			_gameFieldGenerator.Generate(gameField, request.MinesCount);

			var result = new Result()
			{
				GameId = Guid.NewGuid(),
				Field = gameField
			};

			return Task.FromResult(result);
		}
	}
}
