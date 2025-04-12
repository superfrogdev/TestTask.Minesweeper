using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="IGameFieldCreator"/> by random number generation.
	/// </summary>
	public sealed class GameFieldCreatorByRandom : IGameFieldCreator
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

		private readonly Random _random;

		/// <summary>
		/// Initializes a new instance of <see cref="GameFieldCreatorByRandom"/>.
		/// </summary>
		/// <param name="random">Instance of <see cref="Random"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="random"/> cannot be <see langword="null"/>.</exception>
		public GameFieldCreatorByRandom(Random random)
			: base()
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
		}

		/// <inheritdoc/>
		public Cell[,] Create(Size2d fieldSize, ushort minesCount)
		{
			if (fieldSize.CalculateArea() < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(fieldSize), "Area must be greater than zero.");
			}

			if (minesCount > fieldSize.CalculateArea() - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(minesCount), "Count of mines must be equal or less than area of field minus one.");
			}

			var cells = new Cell[fieldSize.Height, fieldSize.Width];

			PutMines(cells, minesCount, _random);

			CalculateCellValues(cells);

			return cells;
		}

		private static void CalculateCellValues(Cell[,] cells)
		{
			var fieldBoundRectangle = new Rectangle(Point2d.Zero, new Size2d((ushort)cells.GetLength(1), (ushort)cells.GetLength(0)));

			for (var y = 0; y < cells.GetLength(0); y++)
			{
				for (var x = 0; x < cells.GetLength(1); x++)
				{
					ref var cell = ref cells[y, x];

					if (cell.Value != Enums.CellValue.Mine)
					{
						byte nearMineCount = 0;

						foreach (var currentTranslate in _translateSizes)
						{
							var targetCoordinates = new Point2d((short)(x + currentTranslate.X), (short)(y + currentTranslate.Y));

							if (fieldBoundRectangle.Contains(targetCoordinates))
							{
								var targetCell = cells[targetCoordinates.Y, targetCoordinates.X];

								if (targetCell.Value == Enums.CellValue.Mine)
								{
									nearMineCount++;
								}
							}
						}

						cell.Value = (Enums.CellValue)nearMineCount;
					}
				}
			}
		}

		private static void PutMines(Cell[,] cells, ushort minesCount, Random random)
		{
			while (minesCount != 0)
			{
				var x = random.Next(0, cells.GetLength(1));
				var y = random.Next(0, cells.GetLength(0));

				ref var cell = ref cells[y, x];

				if (cell.Value == Enums.CellValue.Mine)
				{
					continue;
				}

				cell.Value = Enums.CellValue.Mine;

				--minesCount;
			}
		}
	}
}
