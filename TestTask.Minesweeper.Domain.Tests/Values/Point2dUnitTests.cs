using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Values
{
	/// <summary>
	/// Represents unit tests for <see cref="Point2d"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class Point2dUnitTests
	{
		/// <summary>
		/// Tests <see cref="Point2d.Point2d(short, short)"/>.
		/// </summary>
		/// <param name="x">Coordinate on X-axis.</param>
		/// <param name="y">Coordinate on Y-axis.</param>
		[Theory]
		[InlineData((short)4, (short)5)]
		[InlineData((short)-4030, short.MaxValue)]
		[InlineData(short.MaxValue, short.MaxValue)]
		[InlineData(short.MinValue, short.MinValue)]
		public void Constructor_Initialization_Valid(short x, short y)
		{
			var instance = new Point2d(x, y);

			Assert.True(instance.X == x && instance.Y == y);
		}

		/// <summary>
		/// Tests <see cref="Point2d.Zero"/>.
		/// </summary>
		[Fact]
		public void Zero_Validation_Valid()
		{
			var instance = Point2d.Zero;

			Assert.True(instance.X == 0 && instance.Y == 0);
		}

		/// <summary>
		/// Tests <see cref="Point2d.Equals(Point2d)"/>.
		/// </summary>
		/// <param name="leftX">Coordinate on X-axis of left point.</param>
		/// <param name="leftY">Coordinate on Y-axis of left point.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Point2d.Equals(Point2d)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Coordinate on X-axis of right point.</param>
		/// <param name="rightY">Coordinate on Y-axis of right point.</param>
		[Theory]
		[InlineData((short)-44, (short)57, true, (short)-44, (short)57)]
		[InlineData(short.MinValue, (short)57, true, short.MinValue, (short)57)]
		[InlineData((short)44, (short)57, false, (short)44, (short)557)]
		[InlineData((short)447, (short)57, false, (short)44, (short)57)]
		public void Equals_Validation_Valid(short leftX, short leftY, bool isShouldBeEqual, short rightX, short rightY)
		{
			var left = new Point2d(leftX, leftY);
			var right = new Point2d(rightX, rightY);

			Assert.Multiple(() =>
			{
				Assert.True(left.Equals(right) == isShouldBeEqual);

				Assert.Equal<bool>(isShouldBeEqual, left == right);

				Assert.NotEqual<bool>(isShouldBeEqual, left != right);
			});			
		}

		/// <summary>
		/// Tests <see cref="Point2d.Equals(object)"/>.
		/// </summary>
		/// <param name="leftX">Coordinate on X-axis of left point.</param>
		/// <param name="leftY">Coordinate on Y-axis of left point.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Point2d.Equals(object)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Coordinate on X-axis of right point.</param>
		/// <param name="rightY">Coordinate on Y-axis of right point.</param>
		/// <param name="isSameType">Show if type of right point should be <see cref="Point2d"/>.</param>
		[Theory]
		[InlineData((short)44, (short)-57, true, (short)44, (short)-57, true)]
		[InlineData(short.MinValue, (short)57, true, short.MinValue, (short)57, true)]
		[InlineData((short)44, (short)57, false, (short)44, (short)557, true)]
		[InlineData((short)447, (short)57, false, (short)44, (short)57, true)]
		[InlineData((short)44, (short)57, false, (short)44, (short)57, false)]
		public void EqualsObject_Validation_Valid(short leftX, short leftY, bool isShouldBeEqual, short rightX, short rightY, bool isSameType)
		{
			var left = new Point2d(leftX, leftY);

			object right;

			if (isSameType)
			{
				right = new Point2d(rightX, rightY);
			}
			else
			{
				right = new Size2d((ushort)rightX, (ushort)rightY);
			}

			Assert.True(left.Equals(right) == isShouldBeEqual);
		}

		/// <summary>
		/// Tests <see cref="Point2d.GetHashCode"/>.
		/// </summary>
		[Fact]
		public void GetHashCode_Validation_Valid()
		{
			var first = new Point2d(4, 6);
			var second = new Point2d(4, 6);

			Assert.True(first.GetHashCode() == second.GetHashCode());
		}

		/// <summary>
		/// Tests <see cref="Point2d.ToString"/>
		/// </summary>
		/// <param name="x">Coordinate on X-axis.</param>
		/// <param name="y">Coordinate on Y-axis.</param>
		[Theory]
		[InlineData((short)4, (short)5)]
		[InlineData((short)4030, short.MaxValue)]
		[InlineData(short.MaxValue, short.MaxValue)]
		[InlineData(short.MinValue, short.MinValue)]
		public void ToString_Validation_Valid(short x, short y)
		{
			Assert.Equal(@$"""{nameof(Point2d.X)}"" = ""{x}""; ""{nameof(Point2d.Y)}"" = ""{y}"".", new Point2d(x, y).ToString(), StringComparer.Ordinal);
		}
	}
}
