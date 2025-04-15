namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a result of <see cref="Command"/>.
	/// </summary>
	public sealed class Result
	{
		/// <summary>
		/// Size of game field.
		/// </summary>
		public required Domain.Values.Size2d FieldSize { get; init; }

		/// <summary>
		/// Count of mines.
		/// </summary>
		public required ushort MinesCount { get; init; }

		/// <summary>
		/// Result after the turn was made.
		/// </summary>
		public required Domain.Enums.TurnResult TurnResult{ get; init; }

		/// <summary>
		/// Game field.
		/// </summary>
		public required Domain.Values.Cell[,] Field { get; init; }
	}
}
