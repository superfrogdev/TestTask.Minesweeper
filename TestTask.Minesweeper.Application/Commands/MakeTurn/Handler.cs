using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TestTask.Minesweeper.Persistence;
using TestTask.Minesweeper.Application.Exceptions;

namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a handler of <see cref="Command"/>.
	/// </summary>
	internal sealed class Handler : IRequestHandler<Command, Result>
	{
		private readonly ILogger _logger;
		private readonly Domain.Processors.ITurnProcessor _turnProcessor;
		private readonly GameDbContext _gameDbContext;
		private readonly Domain.Processors.ISnapshotSaveDecisionMaker _snapshotSaveDecisionMaker;

		/// <summary>
		/// Initializes a new instance of <see cref="Handler"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for this instance.</param>
		/// <param name="turnProcessor">Instance of <see cref="Domain.Processors.ITurnProcessor"/>.</param>
		/// <param name="snapshotSaveDecisionMaker">Instance of <see cref="Domain.Processors.ISnapshotSaveDecisionMaker"/>.</param>
		/// <param name="gameDbContext">Instance of <see cref="GameDbContext"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameDbContext"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="turnProcessor"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="snapshotSaveDecisionMaker"/> cannot be <see langword="null"/>.</exception>
		public Handler(
			ILogger<Handler> logger,
			Domain.Processors.ITurnProcessor turnProcessor,
			GameDbContext gameDbContext,
			Domain.Processors.ISnapshotSaveDecisionMaker snapshotSaveDecisionMaker)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_turnProcessor = turnProcessor ?? throw new ArgumentNullException(nameof(turnProcessor));

			_snapshotSaveDecisionMaker = snapshotSaveDecisionMaker ?? throw new ArgumentNullException(nameof(snapshotSaveDecisionMaker));

			_gameDbContext = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
		}

		/// <inheritdoc/>
		public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var gameSession = await _gameDbContext.GameSessions.SingleOrDefaultAsync(predicate => predicate.Id == request.GameId, cancellationToken)
																	.ConfigureAwait(false);

			if (gameSession == null)
			{
				throw new EntityNotFoundException();
			}

			if (gameSession.Status != Domain.Enums.GameSessionStatus.NotCompleted)
			{
				throw new Domain.Exceptions.GameSessionAlreadyCompletedException(gameSession.Id);
			}

			var lastSnapshot = await _gameDbContext.Snapshots.AsNoTracking()
																.Include(navigationPropertyPath => navigationPropertyPath.Turn)
																.Where(predicate => predicate.GameSession == gameSession)
																.OrderBy(keySelector => keySelector.Turn != null ? keySelector.Turn.Number : 0)
																.LastAsync(cancellationToken)
																.ConfigureAwait(false);

			var turnsSinceLastSnapshot = await _gameDbContext.Turns.AsNoTracking()
																	.Where(predicate => predicate.GameSession == gameSession && predicate.Number > (lastSnapshot.Turn != null ? lastSnapshot.Turn.Number : 0))
																	.OrderBy(keySelector => keySelector.Number)
																	.ToListAsync(cancellationToken)
																	.ConfigureAwait(false);

			var newTurn = new Domain.Entities.Turn()
			{
				CellCoordinates = request.SelectedCellCoordinates,
				GameSession = gameSession,
				Number = (ushort)(turnsSinceLastSnapshot.Count > 0 ? turnsSinceLastSnapshot.Last().Number + 1
																   : lastSnapshot.Turn != null ? lastSnapshot.Turn.Number + 1
																							   : 1)
			};

			turnsSinceLastSnapshot.Add(newTurn);

			var (LastTurnResult, lastTurnProcessedCellCount, GameFieldAfterAll) = _turnProcessor.Process(turnsSinceLastSnapshot, lastSnapshot);

			switch (LastTurnResult)
			{
				case Domain.Enums.TurnResult.Defeat:
					gameSession.Status = Domain.Enums.GameSessionStatus.PlayerWasDefeated;
					break;
				case Domain.Enums.TurnResult.Victory:
					gameSession.Status = Domain.Enums.GameSessionStatus.PlayerWon;
					break;
			}

			gameSession.Turns.Add(newTurn);

			if (_snapshotSaveDecisionMaker.IsNeedToBeSaved(LastTurnResult, lastTurnProcessedCellCount))
			{
				var newSnapshot = new Domain.Entities.Snapshot()
				{
					Field = GameFieldAfterAll,
					GameSession = gameSession,
					Turn = newTurn
				};

				gameSession.Snapshots.Add(newSnapshot);
			}

			await _gameDbContext.SaveChangesAsync(cancellationToken)
									.ConfigureAwait(false);

			return new Result()
			{
				Field = GameFieldAfterAll,
				GameSessionStatus = gameSession.Status,
				MinesCount = gameSession.MinesCount
			};
		}
	}
}
