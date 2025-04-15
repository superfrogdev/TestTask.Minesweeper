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
		private readonly Domain.Processors.IGameFieldCreator _gameFieldCreator;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <param name="gameFieldCreator">Instance of <see cref="Domain.Processors.IGameFieldCreator"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameFieldCreator"/> cannot be <see langword="null"/>.</exception>
		public Handler(ILogger<Handler> logger, Domain.Processors.IGameFieldCreator gameFieldCreator)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_gameFieldCreator = gameFieldCreator ?? throw new ArgumentNullException(nameof(gameFieldCreator));
		}

		/// <inheritdoc/>
		public Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var result = new Result()
			{
				GameId = Guid.NewGuid(),
				Field = _gameFieldCreator.Create(request.FieldSize, request.MinesCount)
			};

			return Task.FromResult(result);
		}
	}
}
