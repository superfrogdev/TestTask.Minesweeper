using System.Diagnostics.CodeAnalysis;

namespace TestTask.Minesweeper.Domain.Values
{
	/// <summary>
	/// Represents an axis-aligned rectangle.
	/// </summary>
	public readonly struct Rectangle : IEquatable<Rectangle>
	{
		private static readonly IEqualityComparer<Rectangle> _equalityComparerBySides = new EqualityComparerBySidesImplementation();

		private readonly Size2d _size;
		private readonly Point2d _topLeft;

		/// <summary>
		/// Initializes a new instance of <see cref="Rectangle"/>.
		/// </summary>
		/// <param name="topLeft">See <see cref="Rectangle.TopLeft"/>.</param>
		/// <param name="size">See <see cref="Rectangle.Size"/>.</param>
		/// <exception cref="OverflowException">Too large value for coordinates.</exception>
		public Rectangle(Point2d topLeft, Size2d size)
		{
			ThrowIfIsOverflow(topLeft.X, size.Width);

			ThrowIfIsOverflow(topLeft.Y, size.Height);

			_topLeft = topLeft;

			_size = size;
		}

		/// <summary>
		/// <inheritdoc cref="Rectangle.Rectangle(Point2d, Size2d)"/>
		/// </summary>
		/// <param name="x">See <see cref="Rectangle.X"/>.</param>
		/// <param name="y">See <see cref="Rectangle.Y"/>.</param>
		/// <param name="width">See <see cref="Rectangle.Width"/>.</param>
		/// <param name="height">See <see cref="Rectangle.Height"/>.</param>
		/// <exception cref="OverflowException">Too large value for coordinates.</exception>
		public Rectangle(short x, short y, ushort width, ushort height)
			: this(new Point2d(x, y), new Size2d(width, height))
		{ }

		/// <summary>
		/// <inheritdoc cref="Rectangle.Rectangle(Point2d, Size2d)"/>
		/// </summary>
		/// <param name="x">See <see cref="Rectangle.X"/>.</param>
		/// <param name="y">See <see cref="Rectangle.Y"/>.</param>
		/// <param name="side">Value for both <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/>.</param>
		/// <exception cref="OverflowException">Too large value for coordinates.</exception>
		public Rectangle(short x, short y, ushort side)
			: this(x, y, side, side)
		{ }

		private static void ThrowIfIsOverflow(short topLeft, ushort size)
		{
			var forCheck = topLeft + size;

			if (forCheck > short.MaxValue)
			{
				throw new OverflowException("Too large value for coordinates.");
			}
		}

		/// <summary>
		/// Checks if <paramref name="left"/> intersects with <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance of <see cref="Rectangle"/>.</param>
		/// <param name="right">Instance of <see cref="Rectangle"/>.</param>
		/// <returns><see langword="true"/> - intersects; otherwise - <see langword="false"/>.</returns>
		public static bool IsIntersects(Rectangle left, Rectangle right)
		{
			return right.X < left.X + left.Width
					&& left.X < right.X + right.Width
					&& right.Y < left.Y + left.Height
					&& left.Y < right.Y + right.Height;
		}

		/// <summary>
		/// Checks if this instance intersects with <paramref name="other"/>.
		/// </summary>
		/// <param name="other">Instance of <see cref="Rectangle"/>.</param>
		/// <returns><see langword="true"/> - intersects; otherwise - <see langword="false"/>.</returns>
		public bool IsIntersectsWith(Rectangle other)
		{
			return Rectangle.IsIntersects(this, other);
		}

		/// <summary>
		/// Checks if this instance contains <paramref name="point"/>.
		/// </summary>
		/// <param name="point">Instance of <see cref="Point2d"/>.</param>
		/// <returns><see langword="true"/> - contains; otherwise - <see langword="false"/>.</returns>
		public bool Contains(Point2d point)
		{
			return this.X <= point.X
					&& point.X < this.X + this.Width
					&& this.Y <= point.Y
					&& point.Y < this.Y + this.Height;
		}

		/// <summary>
		/// Checks if this instance contains <paramref name="rectangle"/>.
		/// </summary>
		/// <param name="point">Instance of <see cref="Rectangle"/>.</param>
		/// <returns><see langword="true"/> - contains; otherwise - <see langword="false"/>.</returns>
		public bool Contains(Rectangle rectangle)
		{
			return this.X <= rectangle.X
					&& rectangle.X + rectangle.Width <= this.X + this.Width
					&& this.Y <= rectangle.Y
					&& rectangle.Y + rectangle.Height <= this.Y + this.Height;
		}

		/// <summary>
		/// Gets an <see cref="IEqualityComparer{T}"/> for <see cref="Rectangle"/> by <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/>.
		/// </summary>
		public static IEqualityComparer<Rectangle> EqualityComparerBySides
		{
			get
			{
				return _equalityComparerBySides;
			}
		}

		/// <summary>
		/// <see cref="Point2d.X"/> from <see cref="Rectangle.TopLeft"/>.
		/// </summary>
		public short X
		{
			get
			{
				return _topLeft.X;
			}
		}

		/// <summary>
		/// <see cref="Point2d.Y"/> from <see cref="Rectangle.TopLeft"/>.
		/// </summary>
		public short Y
		{
			get
			{
				return _topLeft.Y;
			}
		}

		/// <summary>
		/// <see cref="Size2d.Width"/> from <see cref="Rectangle.Size"/>.
		/// </summary>
		public ushort Width
		{
			get
			{
				return _size.Width;
			}
		}

		/// <summary>
		/// <see cref="Size2d.Height"/> from <see cref="Rectangle.Size"/>.
		/// </summary>
		public ushort Height
		{
			get
			{
				return _size.Height;
			}
		}

		/// <summary>
		/// Coordinates of top-left corner of this instance.
		/// </summary>
		public Point2d TopLeft
		{
			get
			{
				return _topLeft;
			}
		}

		/// <summary>
		/// Coordinates of top-right corner of this instance.
		/// </summary>
		public Point2d TopRight
		{
			get
			{
				return new Point2d((short)(_topLeft.X + _size.Width), _topLeft.Y);
			}
		}

		/// <summary>
		/// Coordinates of bottom-left corner of this instance.
		/// </summary>
		public Point2d BottomLeft
		{
			get
			{
				return new Point2d(_topLeft.X, (short)(_topLeft.Y + _size.Height));
			}
		}

		/// <summary>
		/// Coordinates of bottom-right corner of this instance.
		/// </summary>
		public Point2d BottomRight
		{
			get
			{
				return new Point2d((short)(_topLeft.X + _size.Width), (short)(_topLeft.Y + _size.Height));
			}
		}

		/// <summary>
		/// Size of sides of this instance.
		/// </summary>
		public Size2d Size
		{
			get
			{
				return _size;
			}
		}

		/// <inheritdoc/>
		public bool Equals(Rectangle other)
		{
			return this._topLeft == other._topLeft
					&& this._size == other._size;
		}

		/// <inheritdoc/>
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is Rectangle other)
			{
				return this.Equals(other);
			}
			else
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(_topLeft, _size);
		}

		/// <summary>
		/// Gets string representation of this instance.
		/// </summary>
		/// <returns><see cref="string"/> with data(<see cref="Rectangle.X"/>, <see cref="Rectangle.Y"/>, <see cref="Rectangle.Width"/>, <see cref="Rectangle.Height"/>) of this instance. 
		/// Example - ""X" = "3"; "Y" = "55"; "Width" = "209"; "Height" = "78".".</returns>
		public override string ToString()
		{
			return $@"""{nameof(X)}"" = ""{X}""; ""{nameof(Y)}"" = ""{Y}""; ""{nameof(Width)}"" = ""{Width}""; ""{nameof(Height)}"" = ""{Height}"".";
		}

		/// <summary>
		/// Checks equality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Checks inequality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - not equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Represents an <see cref="IEqualityComparer{T}"/> for <see cref="Rectangle"/> by <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/>.
		/// </summary>
		internal sealed class EqualityComparerBySidesImplementation : IEqualityComparer<Rectangle>
		{
			/// <summary>
			/// Initializes a new instance of <see cref="EqualityComparerBySidesImplementation"/>.
			/// </summary>
			public EqualityComparerBySidesImplementation()
				: base()
			{ }

			/// <inheritdoc/>
			public bool Equals(Rectangle x, Rectangle y)
			{
				return x._size == y._size;
			}

			/// <inheritdoc/>
			public int GetHashCode([DisallowNull] Rectangle obj)
			{
				return obj._size.GetHashCode();
			}
		}
	}
}
