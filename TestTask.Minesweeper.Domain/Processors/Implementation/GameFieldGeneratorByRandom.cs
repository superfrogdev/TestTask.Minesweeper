using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="IGameFieldGenerator"/> by random number generation.
	/// </summary>
	public sealed class GameFieldGeneratorByRandom : IGameFieldGenerator
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
		/// Initializes a new instance of <see cref="GameFieldGeneratorByRandom"/>.
		/// </summary>
		/// <param name="random">Instance of <see cref="Random"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="random"/> cannot be <see langword="null"/>.</exception>
		public GameFieldGeneratorByRandom(Random random)
			: base()
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
		}

		/// <inheritdoc/>
		public void Generate(GameField gameField, ushort minesCount)
		{
			ArgumentNullException.ThrowIfNull(gameField, nameof(gameField));

			if (minesCount > gameField.Size.CalculateArea() - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(minesCount), "Count of mines must be equal or less than area of field minus one.");
			}

			PutMines(gameField, minesCount, _random);

			CalculateCellValues(gameField);
		}

		private static void CalculateCellValues(GameField gameField)
		{
			var fieldBoundRectangle = new Rectangle(Point2d.Zero, gameField.Size);

			for (var y = 0; y < fieldBoundRectangle.Height; y++)
			{
				for (var x = 0; x < fieldBoundRectangle.Width; x++)
				{
					ref var cell = ref gameField[x, y];

					if (cell.Value != Enums.CellValue.Mine)
					{
						byte nearMineCount = 0;

						foreach (var currentTranslate in _translateSizes)
						{
							var targetCoordinates = new Point2d((short)(x + currentTranslate.X), (short)(y + currentTranslate.Y));

							if (fieldBoundRectangle.Contains(targetCoordinates))
							{
								var targetCell = gameField[targetCoordinates];

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

		private static void PutMines(GameField gameField, ushort minesCount, Random random)
		{
			gameField.Cells.Slice(0, minesCount)
							.Fill(new Cell(Enums.CellValue.Mine, false));

			var shuffleCount = gameField.Count >> 1;

			for (; shuffleCount > 0; shuffleCount--)
			{
				var firstToExchangeIndex = random.Next(0, gameField.Count);

				var secondToExchangeIndex = random.Next(0, gameField.Count);

				(gameField[secondToExchangeIndex], gameField[firstToExchangeIndex]) = (gameField[firstToExchangeIndex], gameField[secondToExchangeIndex]);
			}
		}
	}
}
