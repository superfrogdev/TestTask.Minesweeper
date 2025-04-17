using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Domain.Tests.Values
{
	/// <summary>
	/// Represents unit tests for <see cref="Size2d"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public sealed class Size2dUnitTests
	{
		/// <summary>
		/// Tests <see cref="Size2d.Size2d(ushort, ushort)"/> for parameters validation checks.
		/// </summary>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		[Theory]
		[InlineData((ushort)4, (ushort)5)]
		[InlineData((ushort)4030, ushort.MaxValue)]
		[InlineData(ushort.MaxValue, ushort.MaxValue)]
		[InlineData(ushort.MinValue, ushort.MinValue)]
		public void Constructor_Validation_Valid(ushort width, ushort height)
		{
			var instance = new Size2d(width, height);

			Assert.True(instance.Width == width && instance.Height == height);
		}

		/// <summary>
		/// Tests <see cref="Size2d.Size2d(ushort)"/>.
		/// </summary>
		/// <param name="value">Size for both axes.</param>
		[Theory]
		[InlineData((ushort)4)]
		[InlineData((ushort)4030)]
		[InlineData(ushort.MaxValue)]
		[InlineData(ushort.MinValue)]
		public void ConstructorWithValue_Validation_Valid(ushort value)
		{
			var instance = new Size2d(value);

			Assert.True(instance.Width == value && instance.Height == value);
		}

		/// <summary>
		/// Tests <see cref="Size2d.Zero"/>.
		/// </summary>
		[Fact]
		public void Zero_Validation_Valid()
		{
			var instance = Size2d.Zero;

			Assert.True(instance.Width == ushort.MinValue && instance.Width == ushort.MinValue);
		}

		/// <summary>
		/// Tests <see cref="Size2d.Size2d(ushort)"/>.
		/// </summary>
		/// <param name="value">Size for both axes.</param>
		[Theory]
		[InlineData((ushort)4)]
		[InlineData((ushort)4030)]
		[InlineData(ushort.MaxValue)]
		[InlineData(ushort.MinValue)]
		public void ExplicitOperator_Validation_Valid(ushort value)
		{
			var instance = (Size2d)value;

			Assert.True(instance.Width == value && instance.Height == value);
		}

		/// <summary>
		/// Tests <see cref="Size2d.Equals(Size2d)"/>.
		/// </summary>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Size2d.Equals(Size2d)"/> should be <see langword="true"/>.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		[Theory]
		[InlineData((ushort)44, (ushort)57, true, (ushort)44, (ushort)57)]
		[InlineData(ushort.MinValue, (ushort)57, true, ushort.MinValue, (ushort)57)]
		[InlineData((ushort)44, (ushort)57, false, (ushort)44, (ushort)557)]
		[InlineData((ushort)447, (ushort)57, false, (ushort)44, (ushort)57)]
		public void Equals_Validation_Valid(ushort leftWidth, ushort leftHeight, bool isShouldBeEqual, ushort rightWidth, ushort rightHeight)
		{
			var left = new Size2d(leftWidth, leftHeight);
			var right = new Size2d(rightWidth, rightHeight);

			Assert.Multiple(() =>
			{
				Assert.True(left.Equals(right) == isShouldBeEqual);

				Assert.Equal<bool>(isShouldBeEqual, left == right);

				Assert.NotEqual<bool>(isShouldBeEqual, left != right);
			});			
		}

		/// <summary>
		/// Tests <see cref="Size2d.Equals(object)"/>.
		/// </summary>
		/// <param name="leftWidth">Size by X-axis of left instance.</param>
		/// <param name="leftHeight">Size by Y-axis of left instance.</param>
		/// <param name="isShouldBeEqual">Show if result of <see cref="Size2d.Equals(Size2d)"/> should be <see langword="true"/>.</param>
		/// <param name="rightWidth">Size by X-axis of right instance.</param>
		/// <param name="rightHeight">Size by Y-axis of right instance.</param>
		/// <param name="isSameType">Show if type of right point should be <see cref="Size2d"/>.</param>
		[Theory]
		[InlineData((ushort)44, (ushort)57, true, (ushort)44, (ushort)57, true)]
		[InlineData(ushort.MinValue, (ushort)57, true, ushort.MinValue, (ushort)57, true)]
		[InlineData((ushort)44, (ushort)57, false, (ushort)44, (ushort)557, true)]
		[InlineData((ushort)447, (ushort)57, false, (ushort)44, (ushort)57, true)]
		[InlineData((ushort)44, (ushort)57, false, (ushort)44, (ushort)57, false)]
		public void EqualsAsObject_Validation_Valid(ushort leftWidth, ushort leftHeight, bool isShouldBeEqual, ushort rightWidth, ushort rightHeight, bool isSameType)
		{
			var left = new Size2d(leftWidth, leftHeight);

			object right;

			if (isSameType)
			{
				right = new Size2d(rightWidth, rightHeight);
			}
			else
			{
				right = new Point2d((short)rightWidth, (short)rightHeight);
			}

			Assert.True(left.Equals(right) == isShouldBeEqual);
		}

		/// <summary>
		/// Tests <see cref="Size2d.GetHashCode"/>.
		/// </summary>
		[Fact]
		public void GetHashCode_Validation_Valid()
		{
			var first = new Size2d(478, 600);
			var second = new Size2d(478, 600);

			Assert.True(first.GetHashCode() == second.GetHashCode());
		}

		/// <summary>
		/// Tests <see cref="Size2d.ToString"/>
		/// </summary>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		[Theory]
		[InlineData((ushort)4, (ushort)5)]
		[InlineData((ushort)4030, ushort.MaxValue)]
		[InlineData(ushort.MaxValue, ushort.MaxValue)]
		[InlineData(ushort.MinValue, ushort.MinValue)]
		public void ToString_Validation_Valid(ushort width, ushort height)
		{
			Assert.Equal<string>(@$"""{nameof(Size2d.Width)}"" = ""{width}""; ""{nameof(Size2d.Height)}"" = ""{height}"".", new Size2d(width, height).ToString(), StringComparer.Ordinal);
		}

		/// <summary>
		/// Tests <see cref="Size2d.CalculateArea"/>
		/// </summary>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		[Theory]
		[InlineData((ushort)4, (ushort)5)]
		[InlineData((ushort)4030, ushort.MaxValue)]
		[InlineData(ushort.MaxValue, ushort.MaxValue)]
		[InlineData(ushort.MinValue, ushort.MaxValue)]
		public void CalculateArea_Calculation_Valid(ushort width, ushort height)
		{
			var instance = new Size2d(width, height);

			Assert.True(instance.CalculateArea() == width * (uint)height);
		}

		/// <summary>
		/// Tests <see cref="Size2d.Flip(Size2d)"/>
		/// </summary>
		/// <param name="width">Size by X-axis.</param>
		/// <param name="height">Size by Y-axis.</param>
		[Theory]
		[InlineData((ushort)4, (ushort)5)]
		[InlineData((ushort)4030, ushort.MaxValue)]
		public void Flip_Validation_Valid(ushort width, ushort height)
		{
			var instance = new Size2d(width, height);
			var flippedInstance = new Size2d(height, width);

			Assert.True(Size2d.Flip(instance) == flippedInstance);
		}
	}
}
