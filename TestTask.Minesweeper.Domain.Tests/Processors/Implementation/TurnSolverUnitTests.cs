using Moq;

using TestTask.Minesweeper.Domain.Entities;
using TestTask.Minesweeper.Domain.Enums;
using TestTask.Minesweeper.Domain.Processors;
using TestTask.Minesweeper.Domain.Processors.Implementation;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Processors.Implementation
{
	/// <summary>
	/// Represents unit tests for <see cref="TurnSolver"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class TurnSolverUnitTests
	{
		private readonly TurnSolver _turnSolver;
		private readonly Mock<IOpenAllLinkedCellsProcessor> _openAllLinkedCellsProcessorMock;
		private readonly Mock<Turn> _turnMock;

		/// <summary>
		/// Initializes a new instance of <see cref="TurnSolverUnitTests"/>.
		/// </summary>
		public TurnSolverUnitTests()
			: base()
		{
			_openAllLinkedCellsProcessorMock = new Mock<IOpenAllLinkedCellsProcessor>();

			_turnMock = new Mock<Turn>();

			_turnSolver = new TurnSolver(_openAllLinkedCellsProcessorMock.Object);
		}

		/// <summary>
		/// Tests <see cref="TurnSolver.Solve(Turn, GameField)"/> for case: must be defeat after selecting cell with mine.
		/// </summary>
		[Fact]
		public void Solve_MineCell_Defeat()
		{
			_turnMock.SetupGet(expression => expression.CellCoordinates)
					 .Returns(new Point2d(1, 1));

			var gameField = new GameField(new Size2d(2));

			gameField[0, 0] = new Cell(CellValue.One, false);
			gameField[0, 1] = new Cell(CellValue.One, false);
			gameField[1, 0] = new Cell(CellValue.One, false);
			gameField[1, 1] = new Cell(CellValue.Mine, false);

			var turnResult = _turnSolver.Solve(_turnMock.Object, gameField, out var _);

			Assert.Equal(TurnResult.Defeat, turnResult);
		}

		/// <summary>
		/// Tests <see cref="TurnSolver.Solve(Turn, GameField)"/> for case: after selecting cell with mine, this cell must become opened.
		/// </summary>
		[Fact]
		public void Solve_MineCell_Opened()
		{
			_turnMock.SetupGet(expression => expression.CellCoordinates)
					 .Returns(new Point2d(1, 1));

			var gameField = new GameField(new Size2d(2));

			gameField[0, 0] = new Cell(CellValue.One, false);
			gameField[0, 1] = new Cell(CellValue.One, false);
			gameField[1, 0] = new Cell(CellValue.One, false);
			gameField[1, 1] = new Cell(CellValue.Mine, false);

			var turnResult = _turnSolver.Solve(_turnMock.Object, gameField, out var _);

			Assert.True(gameField[1, 1].IsOpened);
		}

		/// <summary>
		/// Tests <see cref="TurnSolver.Solve(Turn, GameField)"/> for case: when empty cell is selected, then must open all linked cells.
		/// </summary>
		[Fact]
		public void Solve_EmptyCell_OpenAllLinkedCellsProcessorIsCalled()
		{
			_turnMock.SetupGet(expression => expression.CellCoordinates)
					 .Returns(new Point2d(1, 1));

			var gameField = new GameField(new Size2d(2));

			var turnResult = _turnSolver.Solve(_turnMock.Object, gameField, out var _);

			_openAllLinkedCellsProcessorMock.Verify(expression => expression.Open(It.Is<GameField>(match => match == gameField), It.Is<Point2d>(match => match == new Point2d(1, 1))));
		}

		/// <summary>
		/// Tests <see cref="TurnSolver.Solve(Turn, GameField)"/> for case: after selecting ordinary cell, then it must become opened.
		/// </summary>
		[Fact]
		public void Solve_OrdinaryCell_Opened()
		{
			var selectedCellCoordinates = new Point2d(1, 0);

			_turnMock.SetupGet(expression => expression.CellCoordinates)
					 .Returns(selectedCellCoordinates);

			var gameField = new GameField(new Size2d(2));

			gameField[0, 0] = new Cell(CellValue.One, false);
			gameField[0, 1] = new Cell(CellValue.One, false);
			gameField[1, 0] = new Cell(CellValue.One, false);
			gameField[1, 1] = new Cell(CellValue.Mine, false);

			var turnResult = _turnSolver.Solve(_turnMock.Object, gameField, out var _);

			Assert.True(gameField[selectedCellCoordinates].IsOpened);
		}

		/// <summary>
		/// Tests <see cref="TurnSolver.Solve(Turn, GameField)"/> for case: after selecting last closed cell, player must win.
		/// </summary>
		[Fact]
		public void Solve_OpenLastClosedCell_Victory()
		{
			var selectedCellCoordinates = new Point2d(1, 0);

			_turnMock.SetupGet(expression => expression.CellCoordinates)
					 .Returns(selectedCellCoordinates);

			var gameField = new GameField(new Size2d(2));

			gameField[0, 0] = new Cell(CellValue.One, true);
			gameField[0, 1] = new Cell(CellValue.One, true);
			gameField[1, 0] = new Cell(CellValue.One, false);
			gameField[1, 1] = new Cell(CellValue.Mine, false);

			var turnResult = _turnSolver.Solve(_turnMock.Object, gameField, out var _);

			Assert.Equal(TurnResult.Victory, turnResult);
		}
	}
}
