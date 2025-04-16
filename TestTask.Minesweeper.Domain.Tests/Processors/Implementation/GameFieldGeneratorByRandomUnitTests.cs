using TestTask.Minesweeper.Domain.Processors.Implementation;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Processors.Implementation
{
	/// <summary>
	/// Represents unit tests for <see cref="GameFieldGeneratorByRandom"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class GameFieldGeneratorByRandomUnitTests
	{
		/// <summary>
		/// Tests <see cref="GameFieldGeneratorByRandom.Generate(GameField, ushort)"/> for valid generation.
		/// </summary>
		/// <param name="seed">Value to initialize random number generator.</param>
		[Theory]
		[InlineData(43534)]
		[InlineData(743574857)]
		[InlineData(895659146)]
		[InlineData(-567517689)]
		public void Create_GenerationFieldValidation_Valid(int seed)
		{
			//TODO: Change System.Random to own controlled implementation: what happens if implementation of System.Random will changed? - right, numbers will be other!
			var random = new Random(seed);

			var gameFieldGenerator = new GameFieldGeneratorByRandom(random);

			var width = (ushort)random.Next(2, 31);

			var height = (ushort)random.Next(2, 31);

			var minesCount = (ushort)random.Next(0, width * height);

			var gameField = new GameField(new Size2d(width, height));

			gameFieldGenerator.Generate(gameField, minesCount);

			Assert.Multiple(() =>
			{
				ushort actualMineCount = 0;

				foreach (var current in gameField)
				{
					if (current.Value == Enums.CellValue.Mine)
					{
						++actualMineCount;
					}
				}

				Assert.Equal(minesCount, actualMineCount);

				Point2d[] translateSizes =
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

				var fieldBoundRectangle = new Rectangle(Point2d.Zero, gameField.Size);

				for (var y = 0; y < gameField.Size.Height; y++)
				{
					for (var x = 0; x < gameField.Size.Width; x++)
					{
						var cell = gameField[x, y];

						if (cell.Value != Enums.CellValue.Mine)
						{
							byte nearMineCount = 0;

							foreach (var currentTranslate in translateSizes)
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

							Assert.Equal(nearMineCount, (byte)cell.Value);
						}
					}
				}
			});
		}
	}
}
