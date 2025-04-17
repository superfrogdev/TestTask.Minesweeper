using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="IOpenAllLinkedCellsProcessor"/> through breadth-first search.
	/// </summary>
	public sealed class OpenAllLinkedCellsProcessorThroughBreadthFirstSearch : IOpenAllLinkedCellsProcessor
	{
		private static readonly Point2d[] _translateSizes =
		[
			new(-1, -1),
			new(-1, 0),
			new(-1, 1),
			new(0, 1),
			new(1, 1),
			new(1, 0),
			new(1, -1),
			new(0, -1)
		];

		/// <inheritdoc/>
		public ushort Open(GameField gameField, Point2d startPoint)
		{
			ArgumentNullException.ThrowIfNull(gameField, nameof(gameField));

			var fieldBoundRectangle = new Rectangle(Point2d.Zero, gameField.Size);

			if (!fieldBoundRectangle.Contains(startPoint))
			{
				throw new ArgumentOutOfRangeException(nameof(startPoint), "Out of range of field's size.");
			}

			ushort openedCellCount = 1;

			ref var startCell = ref gameField[startPoint];

			startCell.IsOpened = true;

			var cellCoordinatesToOpen = new Queue<Point2d>((int)(gameField.Size.CalculateArea() >> 2));

			cellCoordinatesToOpen.Enqueue(startPoint);

			do
			{
				var currentCellCoordinates = cellCoordinatesToOpen.Dequeue();

				var currenCell = gameField[currentCellCoordinates];

				if (currenCell.Value == Enums.CellValue.Empty)
				{
					openedCellCount += AddCellsToOpen(currentCellCoordinates, gameField, fieldBoundRectangle, cellCoordinatesToOpen.Enqueue);
				}
			}
			while (cellCoordinatesToOpen.Count > 0);

			return openedCellCount;
		}

		private static ushort AddCellsToOpen(Point2d currentCellCoordinates, GameField gameField, Rectangle fieldBoundRectangle, Action<Point2d> addCellCoordinatesToOpen)
		{
			ushort openedCellCount = 0;

			foreach (var currentTranslate in _translateSizes)
			{
				var targetCoordinates = new Point2d((short)(currentCellCoordinates.X + currentTranslate.X), (short)(currentCellCoordinates.Y + currentTranslate.Y));

				if (fieldBoundRectangle.Contains(targetCoordinates))
				{
					ref var cell = ref gameField[targetCoordinates];

					if (!cell.IsOpened && cell.Value != Enums.CellValue.Mine)
					{
						if (cell.Value == Enums.CellValue.Empty)
						{
							addCellCoordinatesToOpen(targetCoordinates);
						}

						cell.IsOpened = true;

						++openedCellCount;
					}
				}
			}

			return openedCellCount;
		}
	}
}
