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
		private readonly Domain.Processors.ITurnSolver _turnSolver;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		public Handler(ILogger<Handler> logger, Domain.Processors.ITurnSolver turnSolver)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_turnSolver = turnSolver;
		}

		/// <inheritdoc/>
		public Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var gameField = new Domain.Values.GameField(new Domain.Values.Size2d(5));

			var gameSession = new Domain.Entities.GameSession()
			{
				FieldSize = gameField.Size,
				Id = request.GameId,
				MinesCount = 0,
				Status = Domain.Enums.GameSessionStatus.NotCompleted,
				Snapshots = Array.Empty<Domain.Entities.Snapshot>(),
				Turns = Array.Empty<Domain.Entities.Turn>()
			};

			var turnResult = _turnSolver.Solve(new Domain.Entities.Turn() { CellCoordinates = request.SelectedCellCoordinates, Number = 1, GameSession = gameSession}, gameField, out var _);

			switch (turnResult)
			{
				case Domain.Enums.TurnResult.Victory:
					gameSession.Status = Domain.Enums.GameSessionStatus.PlayerWon;
					break;
			}

			var result = new Result()
			{
				Field = gameField,
				GameSessionStatus = gameSession.Status,
				MinesCount = gameSession.MinesCount
			};

			return Task.FromResult(result);
		}
	}
}
