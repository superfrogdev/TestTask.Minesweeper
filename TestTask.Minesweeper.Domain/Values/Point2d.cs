using System.Diagnostics.CodeAnalysis;

namespace TestTask.Minesweeper.Domain.Values
{
	/// <summary>
	/// Represents an integer point on Euclidean plane.
	/// </summary>
	public readonly struct Point2d : IEquatable<Point2d>
	{
		private static readonly Point2d _zero = new(0, 0);

		private readonly short _x, _y;

		/// <summary>
		/// Initializes a new instance of <see cref="Point2d"/>.
		/// </summary>
		/// <param name="x">See <see cref="Point2d.X"/>.</param>
		/// <param name="y">See <see cref="Point2d.Y"/>.</param>
		public Point2d(short x, short y)
		{
			_x = x;
			_y = y;
		}

		/// <summary>
		/// Gets an instance of <see cref="Point2d"/> where <see cref="Point2d.X"/> and <see cref="Point2d.Y"/> are equal to zero.
		/// </summary>
		public static Point2d Zero
		{
			get
			{
				return _zero;
			}
		}

		/// <summary>
		/// Coordinate on X-axis.
		/// </summary>
		public short X
		{
			get
			{
				return _x;
			}
		}

		/// <summary>
		/// Coordinate on Y-axis.
		/// </summary>
		public short Y
		{
			get
			{
				return _y;
			}
		}

		/// <inheritdoc/>
		public bool Equals(Point2d other)
		{
			return this._x == other._x
					&& this._y == other._y;
		}

		/// <inheritdoc/>
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is Point2d other)
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
			return HashCode.Combine(_x, _y);
		}

		/// <summary>
		/// Gets string representation of this instance.
		/// </summary>
		/// <returns><see cref="string"/> with data(<see cref="Point2d.X"/>, <see cref="Point2d.Y"/>) of this instance. 
		/// Example - ""X" = "44"; "Y" = "-15".".</returns>
		public override string ToString()
		{
			return $@"""{nameof(X)}"" = ""{X}""; ""{nameof(Y)}"" = ""{Y}"".";
		}

		/// <summary>
		/// Checks equality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator ==(Point2d left, Point2d right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Checks inequality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - not equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator !=(Point2d left, Point2d right)
		{
			return !left.Equals(right);
		}
	}
}
