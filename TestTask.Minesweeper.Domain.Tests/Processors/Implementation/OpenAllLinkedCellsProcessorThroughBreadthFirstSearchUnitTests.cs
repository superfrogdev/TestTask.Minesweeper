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
		/// Tests <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch.Open(Domain.Values.GameField, Domain.Values.Point2d)"/>.
		/// </summary>
		[Fact]
		public void Open_NullGameField_ThrowException()
		{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Assert.Throws<ArgumentNullException>("gameField", () => _instance.Open(null, Domain.Values.Point2d.Zero));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
		}

		/// <summary>
		/// Tests <see cref="OpenAllLinkedCellsProcessorThroughBreadthFirstSearch.Open(Domain.Values.GameField, Domain.Values.Point2d)"/>.
		/// </summary>
		[Fact]
		public void Open_StartPointOutOfRange_ThrowException()
		{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Assert.Throws<ArgumentOutOfRangeException>("startPoint", () => _instance.Open(new Domain.Values.GameField(new Domain.Values.Size2d(10)), new Domain.Values.Point2d(-1, 5)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
		}
	}
}
