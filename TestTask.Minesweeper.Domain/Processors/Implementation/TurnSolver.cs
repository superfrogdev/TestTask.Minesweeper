using TestTask.Minesweeper.Domain.Entities;
using TestTask.Minesweeper.Domain.Enums;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ITurnSolver"/>.
	/// </summary>
	public sealed class TurnSolver : ITurnSolver
	{
		private readonly IOpenAllLinkedCellsProcessor _openAllLinkedCellsProcessor;

		/// <summary>
		/// Initializes a new instance of <see cref="TurnSolver"/>.
		/// </summary>
		/// <param name="openAllLinkedCellsProcessor">Instance of <see cref="IOpenAllLinkedCellsProcessor"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="openAllLinkedCellsProcessor"/> cannot be <see langword="null"/>.</exception>
		public TurnSolver(IOpenAllLinkedCellsProcessor openAllLinkedCellsProcessor)
			: base()
		{
			_openAllLinkedCellsProcessor = openAllLinkedCellsProcessor ?? throw new ArgumentNullException(nameof(openAllLinkedCellsProcessor));
		}

		/// <inheritdoc/>
		public TurnResult Solve(Turn turn, GameField gameField, out ushort processedCellCount)
		{
			ArgumentNullException.ThrowIfNull(turn, nameof(turn));

			ArgumentNullException.ThrowIfNull(gameField, nameof(gameField));

			if (!gameField.BoundRectangle.Contains(turn.CellCoordinates))
			{
				throw new Exceptions.TurnInvalidException(Exceptions.TurnInvalidException.Reason.TargetCellOutOfGameField);
			}

			ref var cell = ref gameField[turn.CellCoordinates];

			if (cell.IsOpened)
			{
				throw new Exceptions.TurnInvalidException(Exceptions.TurnInvalidException.Reason.TargetCellAlreadyOpened);
			}

			if (cell.Value == CellValue.Mine)
			{
				cell.IsOpened = true;

				processedCellCount = 1;

				return TurnResult.Defeat;
			}

			TurnResult turnResult;			

			if (cell.Value == CellValue.Empty)
			{
				processedCellCount = _openAllLinkedCellsProcessor.Open(gameField, turn.CellCoordinates);

				turnResult = TurnResult.EmptyCellOpened;
			}
			else
			{
				cell.IsOpened = true;

				turnResult = TurnResult.CellWithNumberOpened;

				processedCellCount = 1;
			}

			if (IsAllCellsOpened(gameField))
			{
				turnResult = TurnResult.Victory;
			}

			return turnResult;
		}

		private static bool IsAllCellsOpened(GameField gameField)
		{
			foreach (var cell in gameField)
			{
				if (cell.Value == CellValue.Mine
						|| cell.IsOpened)
				{
					continue;
				}

				return false;
			}

			return true;
		}
	}
}
