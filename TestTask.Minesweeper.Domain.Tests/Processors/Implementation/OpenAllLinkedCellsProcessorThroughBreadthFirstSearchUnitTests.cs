using TestTask.Minesweeper.Domain.Processors.Implementation;

namespace TestTask.Minesweeper.Domain.Tests.Processors.Implementation
{
	/// <summary>
	/// Represents unit tests for <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class OpenAllLinkedCellsProcessorThroughBreadthFirstSearchUnitTests
	{
		private readonly OpenAllLinkedCellsProcessorThroughBreadthFirstSearch _instance;

		/// <summary>
		/// Initializes a new instance of <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearchUnitTests"/>.
		/// </summary>
		public OpenAllLinkedCellsProcessorThroughBreadthFirstSearchUnitTests()
			: base()
		{
			_instance = new OpenAllLinkedCellsProcessorThroughBreadthFirstSearch();
		}

		/// <summary>
		/// Tests <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch.Open(Values.Cell[,], Values.Point2d)"/>.
		/// </summary>
		[Fact]
		public void Open_NullCells_ThrowException()
		{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Assert.Throws<ArgumentNullException>("cells", () => _instance.Open(null, Domain.Values.Point2d.Zero));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
		}

		/// <summary>
		/// Tests <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch.Open(Values.Cell[,], Values.Point2d)"/>.
		/// </summary>
		[Fact]
		public void Open_ZeroFieldSize_ThrowException()
		{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Assert.Throws<ArgumentOutOfRangeException>("cells", () => _instance.Open(new Domain.Values.Cell[1, 0], Domain.Values.Point2d.Zero));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
		}

		/// <summary>
		/// Tests <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch.Open(Values.Cell[,], Values.Point2d)"/>.
		/// </summary>
		[Fact]
		public void Open_StartPointOutOfRange_ThrowException()
		{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Assert.Throws<ArgumentOutOfRangeException>("startPoint", () => _instance.Open(new Domain.Values.Cell[10, 10], new Domain.Values.Point2d(-1, 5)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
		}
	}
}
