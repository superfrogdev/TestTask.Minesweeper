using System.Collections;
using System.Text;

namespace TestTask.Minesweeper.Domain.Values
{
	/// <summary>
	/// Represents a game field.
	/// </summary>
	public sealed class GameField : IEquatable<GameField>, IReadOnlyList<Cell>
	{
		private readonly Size2d _size;
		private readonly Cell[] _cells;

		/// <summary>
		/// Initializes a new instance of <see cref="GameField"/> with specified <paramref name="size"/>.
		/// </summary>
		/// <param name="size">Size of game field.</param>
		/// <exception cref="ArgumentOutOfRangeException"><see cref="Size2d.CalculateArea"/> of <paramref name="size"/> must be greater than zero.</exception>
		public GameField(Size2d size)
			: base()
		{
			var area = size.CalculateArea();
			
			if (area == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(size), "Area must be greater than zero.");
			}

			_size = size;

			_cells = new Cell[area];
		}

		/// <summary>
		/// Initializes a new instance of <see cref="GameField"/> and copy cells from <paramref name="other"/>.
		/// </summary>
		/// <param name="other">Instance of <see cref="GameField"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="other"/> cannot be <see langword="null"/>.</exception>
		public GameField(GameField other)
			: this(other != null ? other.Size : throw new ArgumentNullException(nameof(other)))
		{
			Array.Copy(other._cells, this._cells, other._cells.Length);
		}

		/// <summary>
		/// Cells of this instance.
		/// </summary>
		public Span<Cell> Cells
		{
			get
			{
				return _cells.AsSpan();
			}
		}

		/// <summary>
		/// Gets <see cref="Cell"/> from specified <paramref name="point"/>.
		/// </summary>
		/// <param name="point">Location of <see cref="Cell"/>.</param>
		/// <returns>Reference to <see cref="Cell"/>.</returns>
		public ref Cell this[Point2d point]
		{
			get
			{
				return ref this[point.X, point.Y];
			}
		}

		/// <summary>
		/// Gets <see cref="Cell"/> from specified location(<paramref name="row"/>, <paramref name="column"/>).
		/// </summary>
		/// <param name="row">Row index. Like <see cref="Point2d.Y"/>.</param>
		/// <param name="column">Column index. Like <see cref="Point2d.X"/>.</param>
		/// <returns><inheritdoc cref="this[Point2d]"/></returns>
		public ref Cell this[int column, int row]
		{
			get
			{
				return ref _cells[row * _size.Width + column];
			}
		}

		/// <inheritdoc/>
		public Cell this[int index]
		{
			get
			{
				return _cells[index];
			}
			set
			{
				_cells[index] = value;
			}
		}

		/// <summary>
		/// Gets a reference to <see cref="Cell"/> with specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">Index of <see cref="Cell"/>.</param>
		/// <returns>Reference to <see cref="Cell"/>.</returns>
		public ref Cell GetByIndex(int index)
		{
			return ref _cells[index];
		}

		/// <inheritdoc/>
		public int Count
		{
			get
			{
				return _cells.Length;
			}
		}

		/// <inheritdoc/>
		public IEnumerator<Cell> GetEnumerator()
		{
			IReadOnlyList<Cell> enumeration = _cells;
			
			return enumeration.GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Size of this instance.
		/// </summary>
		public Size2d Size
		{
			get
			{
				return _size;
			}
		}

		/// <summary>
		/// Bound rectangle of this instance.
		/// </summary>
		public Rectangle BoundRectangle
		{
			get
			{
				return new Rectangle(Point2d.Zero, _size);
			}
		}

		/// <inheritdoc/>
		public bool Equals(GameField? other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}

			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			
			return this._size == other._size
					&& MemoryExtensions.SequenceEqual(this._cells.AsSpan(), other._cells.AsSpan());
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is GameField other
					&& this.Equals(other);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			var hashCodeBuilder = new HashCode();

			hashCodeBuilder.Add(_size);

			foreach (var cell in _cells)
			{
				hashCodeBuilder.Add(cell);
			}

			return hashCodeBuilder.ToHashCode();
		}

		/// <summary>
		/// Gets string representation of this instance.
		/// </summary>
		/// <returns><see cref="string"/> with data(<see cref="GameField.Size"/>, <see cref="GameField.Cells"/>) of this instance. 
		public override string ToString()
		{
			var cellsStringBuilder = new StringBuilder();

			foreach (var cell in _cells)
			{
				cellsStringBuilder.Append(cell.ToString()); //TODO: How about to remove generation of string? May be, Span?
			}

			return $@"""{nameof(Size)}"" = ""{Size}""; ""{nameof(Cells)}"" = ""{cellsStringBuilder}"".";
		}

		/// <summary>
		/// Checks equality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator ==(GameField? left, GameField? right)
		{
			return EqualityComparer<GameField>.Default.Equals(left, right);
		}

		/// <summary>
		/// Checks inequality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - not equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator !=(GameField? left, GameField? right)
		{
			return !EqualityComparer<GameField>.Default.Equals(left, right);
		}
	}
}
