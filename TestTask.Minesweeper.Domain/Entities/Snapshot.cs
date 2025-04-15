namespace TestTask.Minesweeper.Domain.Entities
{
	/// <summary>
	/// Represents a snapshot of <see cref="Entities.GameSession"/>.
	/// </summary>
	public sealed class Snapshot
	{
		/// <summary>
		/// Identifier of <see cref="GameSession"/>
		/// </summary>
		public required Guid GameSessionId { get; init; }

		/// <summary>
		/// Instance of <see cref="Entities.GameSession"/>.
		/// </summary>
		public GameSession GameSession { get; set; } = default!;

		/// <summary>
		/// Number of <see cref="Turn"/>.
		/// </summary>
		/// <remarks>If <see langword="null"/>, that means no one turn is made.</remarks>
		public required ushort? TurnNumber { get; init; }

		/// <summary>
		/// Instance of <see cref="Entities.Turn"/>.
		/// </summary>
		public Turn? Turn { get; set; } = default;

		/// <summary>
		/// Field of <see cref="Values.Cell"/>.
		/// </summary>
		public required Values.Cell[,] Cells { get; init; }
	}
}
