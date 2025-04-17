namespace TestTask.Minesweeper.Domain.Enums
{
	/// <summary>
	/// Represents a value of <see cref="Values.Cell"/>.
	/// </summary>
	public enum CellValue : byte
	{
		/// <summary>
		/// Empty or "zero".
		/// </summary>
		Empty = 0,

		/// <summary>
		/// One mine near or "one".
		/// </summary>
		One = 1,

		/// <summary>
		/// Two mines near or "two".
		/// </summary>
		Two = 2,

		/// <summary>
		/// Three mines near or "three".
		/// </summary>
		Three = 3,

		/// <summary>
		/// Four mines near or "four".
		/// </summary>
		Four = 4,

		/// <summary>
		/// Five mines near or "five".
		/// </summary>
		Five = 5,

		/// <summary>
		/// Six mines near or "six".
		/// </summary>
		Six = 6,

		/// <summary>
		/// Seven mines near or "seven".
		/// </summary>
		Seven = 7,

		/// <summary>
		/// Eight mines near or "eight".
		/// </summary>
		Eight = 8,

		/// <summary>
		/// Mine here.
		/// </summary>
		Mine = 9
	}
}
