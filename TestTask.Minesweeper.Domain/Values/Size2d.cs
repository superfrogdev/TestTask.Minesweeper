using System.Diagnostics.CodeAnalysis;

namespace TestTask.Minesweeper.Domain.Values
{
	/// <summary>
	/// Represents a two-dimensional integer size.
	/// </summary>
	public readonly struct Size2d : IEquatable<Size2d>
	{
		private static readonly Size2d _zero = new(0);

		private readonly ushort _width, _height;

		/// <summary>
		/// Initializes a new instance of <see cref="Size2d"/>.
		/// </summary>
		/// <param name="width">See <see cref="Size2d.Width"/>.</param>
		/// <param name="height">See <see cref="Size2d.Height"/>.</param>
		public Size2d(ushort width, ushort height)
		{
			_width = width;
			_height = height;
		}

		/// <summary>
		/// <inheritdoc cref="Size2d.Size2d(ushort, ushort)"/>>
		/// </summary>
		/// <param name="value">Value of size for both <see cref="Size2d.Width"/> and <see cref="Size2d.Height"/>.</param>
		public Size2d(ushort value)
			: this(value, value)
		{ }

		/// <summary>
		/// Gets an instance of <see cref="Size2d"/> where <see cref="Size2d.Width"/> and <see cref="Size2d.Height"/> are equal to zero.
		/// </summary>
		public static Size2d Zero
		{
			get
			{
				return _zero;
			}
		}

		/// <summary>
		/// Flips <see cref="Size2d.Width"/> and <see cref="Size2d.Height"/>.
		/// </summary>
		/// <param name="value">Instance of <see cref="Size2d"/> to flip.</param>
		/// <returns>Instance of <see cref="Size2d"/> which contains flipped values.</returns>
		public static Size2d Flip(Size2d value)
		{
			return new Size2d(value.Height, value.Width);
		}

		/// <summary>
		/// Size by X-axis.
		/// </summary>
		public ushort Width
		{
			get
			{
				return _width;
			}
		}

		/// <summary>
		/// Size by Y-axis.
		/// </summary>
		public ushort Height
		{
			get
			{
				return _height;
			}
		}

		/// <summary>
		/// Calculates area of this instance.
		/// </summary>
		/// <returns>Area of this instance.</returns>
		public uint CalculateArea()
		{
			return _width * (uint)_height;
		}

		/// <inheritdoc/>
		public bool Equals(Size2d other)
		{
			return this._width == other._width
					&& this._height == other._height;
		}

		/// <inheritdoc/>
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is Size2d other)
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
			return HashCode.Combine(_width, _height);
		}

		/// <summary>
		/// Gets string representation of this instance.
		/// </summary>
		/// <returns><see cref="string"/> with data(<see cref="Size2d.Width"/>, <see cref="Size2d.Height"/>) of this instance. 
		/// Example - ""Width" = "97"; "Height" = "679".".</returns>
		public override string ToString()
		{
			return $@"""{nameof(Width)}"" = ""{Width}""; ""{nameof(Height)}"" = ""{Height}"".";
		}

		/// <summary>
		/// Checks equality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator ==(Size2d left, Size2d right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Checks inequality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - not equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator !=(Size2d left, Size2d right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Initializes a new instance of <see cref="Size2d"/> from <paramref name="value"/>.
		/// </summary>
		/// <param name="value"><inheritdoc cref="Size2d.Size2d(ushort)" path="/param[@name='value']"/></param>
		public static explicit operator Size2d(ushort value)
		{
			return new Size2d(value);
		}
	}
}
