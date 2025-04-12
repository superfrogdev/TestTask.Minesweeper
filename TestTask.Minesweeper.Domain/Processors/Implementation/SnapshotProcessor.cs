using TestTask.Minesweeper.Domain.Entities;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// 
	/// </summary>
	internal sealed class SnapshotProcessor : ISnapshotProcessor
	{
		private readonly IOpenAllLinkedCellsProcessor _openAllLinkedCellsProcessor;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="openAllLinkedCellsProcessor"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public SnapshotProcessor(IOpenAllLinkedCellsProcessor openAllLinkedCellsProcessor)
			: base()
		{
			_openAllLinkedCellsProcessor = openAllLinkedCellsProcessor ?? throw new ArgumentNullException(nameof(openAllLinkedCellsProcessor));
		}

		/// <inheritdoc/>
		public Snapshot Process(Snapshot previousSnapshot, Turn newTurn, out ITurnProcessor.ResultOfProcess newTurnResult)
		{
			ArgumentNullException.ThrowIfNull(previousSnapshot, nameof(previousSnapshot));

			ArgumentNullException.ThrowIfNull(newTurn, nameof(newTurn));

			var cells = previousSnapshot.Cells;

			var selectedCell = cells[newTurn.CellCoordinates.Y, newTurn.CellCoordinates.X];

			if (selectedCell.IsOpened)
			{
				throw new Exception("Already opened.");
			}

			if (selectedCell.Value == Enums.CellValue.Mine)
			{
				newTurnResult = ITurnProcessor.ResultOfProcess.Defeat;

				return MakeCopy(previousSnapshot, newTurn);
			}

			var newSnapshot = MakeCopy(previousSnapshot, newTurn);

			if (selectedCell.Value == Enums.CellValue.Empty)
			{
				var _ = _openAllLinkedCellsProcessor.Open(newSnapshot.Cells, newTurn.CellCoordinates);

				newTurnResult = ITurnProcessor.ResultOfProcess.EmptyCellOpened;
			}
			else
			{
				ref var cellToOpen = ref newSnapshot.Cells[newTurn.CellCoordinates.Y, newTurn.CellCoordinates.X];

				cellToOpen.IsOpened = true;

				newTurnResult = ITurnProcessor.ResultOfProcess.CellWithNumberOpened;
			}

			if (IsAllOpened(newSnapshot.Cells))
			{
				newTurnResult = ITurnProcessor.ResultOfProcess.Victory;
			}

			return newSnapshot;
		}

		private static bool IsAllOpened(Cell[,] cells)
		{
			foreach (var current in cells)
			{
				if (current.Value == Enums.CellValue.Mine || current.IsOpened)
				{
					continue;
				}

				return false;
			}

			return true;
		}

		private static Snapshot MakeCopy(Snapshot previousSnapshot, Turn newTurn)
		{
			var newSnapshot = new Snapshot()
			{
				GameSessionId = previousSnapshot.GameSessionId,
				TurnNumber = newTurn.Number,
				Cells = new Cell[previousSnapshot.Cells.GetLength(0), previousSnapshot.Cells.GetLength(1)]
			};

			Array.Copy(previousSnapshot.Cells, newSnapshot.Cells, previousSnapshot.Cells.Length);

			return newSnapshot;
		}
	}
}
