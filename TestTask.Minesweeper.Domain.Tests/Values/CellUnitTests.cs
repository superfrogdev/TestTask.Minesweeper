using System.Runtime.InteropServices;

using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Values
{
	/// <summary>
	/// Represents unit tests for <see cref="Cell"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class CellUnitTests
	{
		/// <summary>
		/// Tests <see cref="Cell.Cell(Enums.CellValue, bool)"/>.
		/// </summary>
		/// <param name="value">See <see cref="Cell.Value"/>.</param>
		/// <param name="isOpened">See <see cref="Cell.IsOpened"/>.</param>
		[Theory]
		[InlineData((byte)Enums.CellValue.Empty, false)]
		[InlineData((byte)Enums.CellValue.One, false)]
		[InlineData((byte)Enums.CellValue.Mine, false)]
		[InlineData((byte)Enums.CellValue.Mine, true)]
		[InlineData((byte)Enums.CellValue.Eight, true)]
		public void Constructor_Initialization_Valid(byte value, bool isOpened)
		{
			var instance = new Cell((Enums.CellValue)value, isOpened);

			Assert.Multiple(() =>
			{
				Assert.Equal((Enums.CellValue)value, instance.Value);

				Assert.Equal(isOpened, instance.IsOpened);
			});
		}

		/// <summary>
		/// Tests <see cref="Cell"/>'s size in <see cref="byte"/>s.
		/// </summary>
		[Fact]
		public void CellSizeInBytes_RepresentsAsByte_Valid()
		{
			var sizeInBytes = Marshal.SizeOf<Cell>();

			Assert.Equal(1, sizeInBytes);
		}
	}
}
