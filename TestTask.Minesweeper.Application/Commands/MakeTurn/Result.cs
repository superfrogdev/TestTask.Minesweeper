namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a result of <see cref="Command"/>.
	/// </summary>
	public sealed class Result
	{
		/// <summary>
		/// Width of game field.
		/// </summary>
		public required ushort Width { get; init; }

		/// <summary>
		/// Height of game field.
		/// </summary>
		public required ushort Height { get; init; }

		/// <summary>
		/// Count of mines.
		/// </summary>
		public required ushort MinesCount { get; init; }

		/// <summary>
		/// Indicates that game is completed.
		/// </summary>
		public required bool IsCompleted { get; init; }

		/// <summary>
		/// Game field.
		/// </summary>
		public required byte[,] Field { get; init; }
	}
}
