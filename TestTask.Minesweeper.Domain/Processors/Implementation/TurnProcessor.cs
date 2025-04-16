using TestTask.Minesweeper.Domain.Entities;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ITurnProcessor"/>.
	/// </summary>
	public sealed class TurnProcessor : ITurnProcessor
	{
		private readonly ITurnSolver _turnSolver;

		/// <summary>
		/// Initializes a new instance of <see cref="TurnProcessor"/>.
		/// </summary>
		/// <param name="turnSolver">Instance of <see cref="ITurnSolver"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="turnSolver"/> cannot be <see langword="null"/>.</exception>
		public TurnProcessor(ITurnSolver turnSolver)
			: base()
		{
			_turnSolver = turnSolver ?? throw new ArgumentNullException(nameof(turnSolver));
		}

		/// <inheritdoc/>
		public (Enums.TurnResult LastTurnResult, ushort lastTurnProcessedCellCount, GameField GameFieldAfterAll) Process(IEnumerable<Turn> turns, Snapshot lastSnapshot)
		{
			ArgumentNullException.ThrowIfNull(turns, nameof(turns));

			ArgumentNullException.ThrowIfNull(lastSnapshot, nameof(lastSnapshot));

			using (var turnsEnumerator = turns.GetEnumerator())
			{
				if (!turnsEnumerator.MoveNext())
				{
					throw new ArgumentException("Must contains at least one element.", nameof(turns));
				}

				var gameField = new GameField(lastSnapshot.Field);

				Enums.TurnResult lastTurnResult;
				ushort lastTurnProcessedCellCount;

				do
				{
					var currentTurn = turnsEnumerator.Current;

					lastTurnResult = _turnSolver.Solve(currentTurn, gameField, out lastTurnProcessedCellCount);
				}
				while (turnsEnumerator.MoveNext());

				return (lastTurnResult, lastTurnProcessedCellCount, gameField);
			}
		}
	}
}
