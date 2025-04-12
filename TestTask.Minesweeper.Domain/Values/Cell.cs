using System.Diagnostics.CodeAnalysis;

namespace TestTask.Minesweeper.Domain.Values
{
	/// <summary>
	/// Represents a cell of game field.
	/// </summary>
	public struct Cell : IEquatable<Cell>
	{
		private byte _value;

		/// <summary>
		/// Initialize a new instance of <see cref="Cell"/>.
		/// </summary>
		/// <param name="value">See <see cref="Value"/>.</param>
		/// <param name="isOpened">See <see cref="IsOpened"/>.</param>
		public Cell(Enums.CellValue value, bool isOpened)
		{
			Value = value;

			IsOpened = isOpened;
		}

		/// <summary>
		/// Indicates that this instance is opened by player.
		/// </summary>
		public bool IsOpened
		{
			get
			{
				return (_value & 0b10000) > 0;
			}
			set
			{
				_value &= 0b11101111;

				if (value)
				{
					_value |= 0b10000;
				}
			}
		}

		/// <summary>
		/// Value of this instance.
		/// </summary>
		public Enums.CellValue Value
		{
			get
			{
				return (Enums.CellValue)(_value & 0b1111);
			}
			set
			{
				var valueAsByte = (byte)value;

				_value &= 0b11110000;

				_value |= valueAsByte;
			}
		}

		/// <inheritdoc/>
		public bool Equals(Cell other)
		{
			return _value == other._value;
		}

		/// <inheritdoc/>
		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			return obj is Cell other
					&& Equals(other);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		/// <summary>
		/// Gets string representation of this instance.
		/// </summary>
		/// <returns><see cref="string"/> with data(<see cref="Cell.Value"/>, <see cref="Cell.IsOpened"/>) of this instance. 
		/// Example - ""Value" = "Empty"; "Height" = "false".".</returns>
		public override string ToString()
		{
			return @$"""{nameof(Value)}"": ""{Value}""; ""{nameof(IsOpened)}"": ""{IsOpened}"".";
		}

		/// <summary>
		/// Checks equality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator ==(Cell left, Cell right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Checks inequality of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">Instance to check.</param>
		/// <param name="right">Instance to check.</param>
		/// <returns><see langword="true"/> - not equal; otherwise - <see langword="false"/>.</returns>
		public static bool operator !=(Cell left, Cell right)
		{
			return !(left == right);
		}
	}
}
