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
		public ushort Open(Cell[,] cells, Point2d startPoint)
		{
			ArgumentNullException.ThrowIfNull(cells, nameof(cells));

			var fieldSize = new Size2d((ushort)cells.GetLength(1), (ushort)cells.GetLength(0));

			if (fieldSize.CalculateArea() < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(cells), "Area must be greater than zero.");
			}

			var fieldBoundRectangle = new Rectangle(Point2d.Zero, fieldSize);

			if (!fieldBoundRectangle.Contains(startPoint))
			{
				throw new ArgumentOutOfRangeException(nameof(startPoint), "Out of range of field's size.");
			}

			ushort openedCellCount = 1;

			ref var startCell = ref cells[startPoint.Y, startPoint.X];

			startCell.IsOpened = true;

			var cellCoordinatesToOpen = new Queue<Point2d>((int)(fieldSize.CalculateArea() >> 2));

			cellCoordinatesToOpen.Enqueue(startPoint);

			do
			{
				var currentCellCoordinates = cellCoordinatesToOpen.Dequeue();

				var currenCell = cells[currentCellCoordinates.Y, currentCellCoordinates.X];

				if (currenCell.Value == Enums.CellValue.Empty)
				{
					openedCellCount += AddCellsToOpen(currentCellCoordinates, cells, fieldBoundRectangle, cellCoordinatesToOpen.Enqueue);
				}
			}
			while (cellCoordinatesToOpen.Count > 0);

			return openedCellCount;
		}

		private static ushort AddCellsToOpen(Point2d currentCellCoordinates, Cell[,] cells, Rectangle fieldBoundRectangle, Action<Point2d> addCellCoordinatesToOpen)
		{
			ushort openedCellCount = 0;

			foreach (var currentTranslate in _translateSizes)
			{
				var targetCoordinates = new Point2d((short)(currentCellCoordinates.X + currentTranslate.X), (short)(currentCellCoordinates.Y + currentTranslate.Y));

				if (fieldBoundRectangle.Contains(targetCoordinates))
				{
					ref var cell = ref cells[targetCoordinates.Y, targetCoordinates.X];

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
