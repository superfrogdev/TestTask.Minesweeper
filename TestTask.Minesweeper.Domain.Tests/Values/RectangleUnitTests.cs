using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Values
{
	/// <summary>
	/// Represents unit tests for <see cref="Rectangle"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class RectangleUnitTests
	{
		/// <summary>
		/// Tests <see cref="Rectangle.Rectangle(Point2d, Size2d)"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		/// <param name="isShouldOverflow">Show if <see cref="Rectangle.Rectangle(Point2d, Size2d)"/> should throw <see cref="OverflowException"/>.</param>
		[Theory]
		[InlineData((short)47, (short)33, (ushort)4, (ushort)5, false)]
		[InlineData(short.MinValue, (short)333, ushort.MaxValue, (ushort)4030, false)]
		[InlineData((short)47, (short)33, ushort.MaxValue, ushort.MaxValue, true)]
		[InlineData(short.MaxValue, short.MinValue, ushort.MinValue, ushort.MinValue, false)]
		[InlineData(short.MinValue, short.MinValue, ushort.MaxValue, ushort.MaxValue, false)]
		public void ConstructorWithTopLeftAndSize_Initialization_Valid(short x, short y, ushort width, ushort height, bool isShouldOverflow)
		{
			var point = new Point2d(x, y);
			var size = new Size2d(width, height);

			if (isShouldOverflow)
			{
				Assert.Throws<OverflowException>(() => new Rectangle(point, size));
			}
			else
			{
				var rectangle = new Rectangle(point, size);

				Assert.True(rectangle.TopLeft == point && rectangle.Size == size && rectangle.Width == size.Width && rectangle.Height == size.Height);
			}
		}

		/// <summary>
		/// Tests <see cref="Rectangle.Rectangle(short, short, ushort, ushort)"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		/// <param name="isShouldOverflow">Show if <see cref="Rectangle.Rectangle(Point2d, Size2d)"/> should throw <see cref="OverflowException"/>.</param>
		[Theory]
		[InlineData((short)47, (short)33, (ushort)4, (ushort)5, false)]
		[InlineData(short.MinValue, (short)333, ushort.MaxValue, (ushort)4030, false)]
		[InlineData((short)47, (short)33, ushort.MaxValue, ushort.MaxValue, true)]
		[InlineData(short.MaxValue, short.MinValue, ushort.MinValue, ushort.MinValue, false)]
		[InlineData(short.MinValue, short.MinValue, ushort.MaxValue, ushort.MaxValue, false)]
		public void Constructor_Initialization_Valid(short x, short y, ushort width, ushort height, bool isShouldOverflow)
		{
			var point = new Point2d(x, y);
			var size = new Size2d(width, height);

			if (isShouldOverflow)
			{
				Assert.Throws<OverflowException>(() => new Rectangle(x, y, width, height));
			}
			else
			{
				var rectangle = new Rectangle(x, y, width, height);

				Assert.True(rectangle.TopLeft == point && rectangle.Size == size && rectangle.Width == size.Width && rectangle.Height == size.Height);
			}
		}

		/// <summary>
		/// Tests <see cref="Rectangle.TopLeft"/>, <see cref="Rectangle.X"/>, <see cref="Rectangle.Y"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		[Theory]
		[InlineData((short)-44, (short)230)]
		[InlineData((short)4450, (short)-20)]
		public void TopLeft_Initialization_Valid(short x, short y)
		{
			var rectangle = new Rectangle(x, y, 1);

			Assert.Multiple(() =>
			{
				Assert.True(rectangle.TopLeft.X == x && rectangle.TopLeft.Y == y);

				Assert.True(rectangle.X == x && rectangle.Y == y);
			});
		}

		/// <summary>
		/// Tests <see cref="Rectangle.TopRight"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		/// <param name="expectedX">Top-right coordinate on X-axis.</param>
		[Theory]
		[InlineData((short)44, (short)230, (ushort)10, (ushort)10, (short)54)]
		[InlineData((short)1020, (short)230, (ushort)5000, (ushort)10, (short)6020)]
		public void TopRight_Initialization_Valid(short x, short y, ushort width, ushort height, short expectedX)
		{
			var rectangle = new Rectangle(x, y, width, height);

			Assert.True(rectangle.TopRight.X == expectedX && rectangle.TopRight.Y == y);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.BottomLeft"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		/// <param name="expectedY">Bottom-left coordinate on Y-axis.</param>
		[Theory]
		[InlineData((short)44, (short)230, (ushort)10, (ushort)10, (short)240)]
		[InlineData((short)1020, (short)20, (ushort)5000, (ushort)100, (short)120)]
		public void BottomLeft_Initialization_Valid(short x, short y, ushort width, ushort height, short expectedY)
		{
			var rectangle = new Rectangle(x, y, width, height);

			Assert.True(rectangle.BottomLeft.X == x && rectangle.BottomLeft.Y == expectedY);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.BottomRight"/>.
		/// </summary>
		/// <param name="x">Top-left coordinate on X-axis.</param>
		/// <param name="y">Top-left coordinate on Y-axis.</param>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		/// <param name="expectedX">Bottom-right coordinate on X-axis.</param>
		/// <param name="expectedY">Bottom-right coordinate on Y-axis.</param>
		[Theory]
		[InlineData((short)44, (short)230, (ushort)10, (ushort)10, (ushort)54, (short)240)]
		[InlineData((short)1020, (short)2030, (ushort)5000, (ushort)10, (ushort)6020, (short)2040)]
		public void BottomRight_Initialization_Valid(short x, short y, ushort width, ushort height, short expectedX, short expectedY)
		{
			var rectangle = new Rectangle(x, y, width, height);

			Assert.True(rectangle.BottomRight.X == expectedX && rectangle.BottomRight.Y == expectedY);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.Equals(Rectangle)"/>.
		/// </summary>
		/// <param name="leftX">Top-left on X-axis of left instance.</param>
		/// <param name="leftY">Top-left on Y-axis of left instance.</param>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Rectangle.Equals(Rectangle)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Top-left on X-axis of right instance.</param>
		/// <param name="rightY">Top-left on Y-axis of right instance.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		[Theory]
		[InlineData((short)44, (short)57, (ushort)10, (ushort)10, true, (short)44, (short)57, (ushort)10, (ushort)10)]
		[InlineData((short)44, (short)157, (ushort)10, (ushort)120, true, (short)44, (short)157, (ushort)10, (ushort)120)]
		[InlineData((short)44, (short)157, (ushort)10, (ushort)120, false, (short)41, (short)157, (ushort)10, (ushort)120)]
		public void EqualsOtherRectangle_Validation_Valid(short leftX, short leftY, ushort leftWidth, ushort leftHeight, bool isShouldBeEqual, short rightX, short rightY, ushort rightWidth, ushort rightHeight)
		{
			var left = new Rectangle(leftX, leftY, leftWidth, leftHeight);
			var right = new Rectangle(rightX, rightY, rightWidth, rightHeight);

			Assert.Multiple(() =>
			{
				Assert.True(left.Equals(right) == isShouldBeEqual);

				Assert.Equal<bool>(isShouldBeEqual, left == right);

				Assert.NotEqual<bool>(isShouldBeEqual, left != right);
			});
		}

		/// <summary>
		/// Tests <see cref="Rectangle.Equals(Rectangle)"/> with <see cref="Rectangle.EqualityComparerBySides"/>.
		/// </summary>
		/// <param name="leftX">Top-left on X-axis of left instance.</param>
		/// <param name="leftY">Top-left on Y-axis of left instance.</param>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Rectangle.Equals(Rectangle)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Top-left on X-axis of right instance.</param>
		/// <param name="rightY">Top-left on Y-axis of right instance.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		[Theory]
		[InlineData((short)44, (short)57, (ushort)10, (ushort)10, true, (short)44, (short)57, (ushort)10, (ushort)10)]
		[InlineData((short)44, (short)157, (ushort)10, (ushort)120, true, (short)44, (short)157, (ushort)10, (ushort)120)]
		[InlineData((short)444, (short)1457, (ushort)10, (ushort)120, true, (short)44, (short)159, (ushort)10, (ushort)120)]
		[InlineData((short)444, (short)1457, (ushort)10, (ushort)120, false, (short)44, (short)159, (ushort)10, (ushort)110)]
		public void EqualsWithSidesOnly_Validation_Valid(short leftX, short leftY, ushort leftWidth, ushort leftHeight, bool isShouldBeEqual, short rightX, short rightY, ushort rightWidth, ushort rightHeight)
		{
			var left = new Rectangle(leftX, leftY, leftWidth, leftHeight);
			var right = new Rectangle(rightX, rightY, rightWidth, rightHeight);

			Assert.True(Rectangle.EqualityComparerBySides.Equals(left, right) == isShouldBeEqual);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.IsIntersectsWith(Rectangle)"/>.
		/// </summary>
		/// <param name="leftX">Top-left on X-axis of left instance.</param>
		/// <param name="leftY">Top-left on Y-axis of left instance.</param>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeIntersects">Show if result of <see cref="Rectangle.IsIntersectsWith(Rectangle)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Top-left on X-axis of right instance.</param>
		/// <param name="rightY">Top-left on Y-axis of right instance.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		[Theory]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)5, (short)8, (ushort)7, (ushort)4)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, false, (short)10, (short)10, (ushort)7, (ushort)4)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, false, (short)0, (short)10, (ushort)7, (ushort)4)]
		[InlineData((short)110, (short)53, (ushort)10, (ushort)17, true, (short)115, (short)59, (ushort)1, (ushort)1)]
		public void Intersection_Validation_Valid(short leftX, short leftY, ushort leftWidth, ushort leftHeight, bool isShouldBeIntersects, short rightX, short rightY, ushort rightWidth, ushort rightHeight)
		{
			var left = new Rectangle(leftX, leftY, leftWidth, leftHeight);
			var right = new Rectangle(rightX, rightY, rightWidth, rightHeight);

			Assert.True(left.IsIntersectsWith(right) == isShouldBeIntersects);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.Contains(Point2d)"/>.
		/// </summary>
		/// <param name="leftX">Top-left on X-axis of left instance.</param>
		/// <param name="leftY">Top-left on Y-axis of left instance.</param>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeContains">Show if result of <see cref="Rectangle.Contains(Point2d)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Top-left on X-axis of right instance.</param>
		/// <param name="rightY">Top-left on Y-axis of right instance.</param>
		[Theory]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)5, (short)8)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, false, (short)10, (short)10)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)9, (short)9)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)0, (short)0)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)0, (short)9)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)9, (short)0)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)5, (short)5)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, false, (short)15, (short)8)]
		public void ContainsPoint_Validation_Valid(short leftX, short leftY, ushort leftWidth, ushort leftHeight, bool isShouldBeContains, short rightX, short rightY)
		{
			var left = new Rectangle(leftX, leftY, leftWidth, leftHeight);
			var right = new Point2d(rightX, rightY);

			Assert.True(left.Contains(right) == isShouldBeContains);
		}

		/// <summary>
		/// Tests <see cref="Rectangle.Contains(Rectangle)"/>.
		/// </summary>
		/// <param name="leftX">Top-left on X-axis of left instance.</param>
		/// <param name="leftY">Top-left on Y-axis of left instance.</param>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeContains">Show if result of <see cref="Rectangle.Contains(Rectangle)"/> should be <see langword="true"/>.</param>
		/// <param name="rightX">Top-left on X-axis of right instance.</param>
		/// <param name="rightY">Top-left on Y-axis of right instance.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		[Theory]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, false, (short)5, (short)8, (ushort)7, (ushort)4)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)0, (short)0, (ushort)10, (ushort)10)]
		[InlineData((short)0, (short)0, (ushort)10, (ushort)10, true, (short)5, (short)8, (ushort)5, (ushort)2)]
		public void ContainsRectangle_Validation_Valid(short leftX, short leftY, ushort leftWidth, ushort leftHeight, bool isShouldBeContains, short rightX, short rightY, ushort rightWidth, ushort rightHeight)
		{
			var left = new Rectangle(leftX, leftY, leftWidth, leftHeight);
			var right = new Rectangle(rightX, rightY, rightWidth, rightHeight);

			Assert.True(left.Contains(right) == isShouldBeContains);
		}
	}
}
