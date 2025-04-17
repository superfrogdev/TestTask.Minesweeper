using MediatR;

using Microsoft.Extensions.Logging;

using TestTask.Minesweeper.Persistence;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a handler of <see cref="Command"/>.
	/// </summary>
	internal sealed class Handler : IRequestHandler<Command, Result>
	{
		private readonly ILogger _logger;
		private readonly Domain.Processors.IGameFieldGenerator _gameFieldGenerator;
		private readonly GameDbContext _gameDbContext;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <param name="gameFieldGenerator">Instance of <see cref="Domain.Processors.IGameFieldGenerator"/>.</param>
		/// <param name="gameDbContext">Instance of <see cref="GameDbContext"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameFieldGenerator"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameDbContext"/> cannot be <see langword="null"/>.</exception>
		public Handler(ILogger<Handler> logger, Domain.Processors.IGameFieldGenerator gameFieldGenerator, GameDbContext gameDbContext)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_gameFieldGenerator = gameFieldGenerator ?? throw new ArgumentNullException(nameof(gameFieldGenerator));

			_gameDbContext = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
		}

		/// <inheritdoc/>
		public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var gameField = new Domain.Values.GameField(request.FieldSize);

			_gameFieldGenerator.Generate(gameField, request.MinesCount);

			var gameSession = new Domain.Entities.GameSession()
			{
				FieldSize = request.FieldSize,
				Id = default,
				MinesCount = request.MinesCount,
				Status = Domain.Enums.GameSessionStatus.NotCompleted,
				Snapshots = new List<Domain.Entities.Snapshot>(1),
				Turns = new List<Domain.Entities.Turn>(0)
			};

			var initialSnapshot = new Domain.Entities.Snapshot()
			{
				Field = gameField,
				GameSession = gameSession,
				Turn = null
			};

			gameSession.Snapshots.Add(initialSnapshot);

			await _gameDbContext.AddAsync(gameSession, cancellationToken)
								.ConfigureAwait(false);

			await _gameDbContext.SaveChangesAsync(cancellationToken)
								.ConfigureAwait(false);

			return new Result()
			{
				GameId = gameSession.Id,
				Field = gameField
			};
		}
	}
}
