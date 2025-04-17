namespace TestTask.Minesweeper.Domain.Entities
{
	/// <summary>
	/// Represents a snapshot of <see cref="Entities.GameSession"/>.
	/// </summary>
	public class Snapshot
	{
		/// <summary>
		/// Instance of <see cref="Entities.GameSession"/>.
		/// </summary>
		public required GameSession GameSession { get; set; } = default!;

		/// <summary>
		/// Instance of <see cref="Entities.Turn"/>.
		/// </summary>
		public Turn? Turn { get; set; } = default;

		/// <summary>
		/// Instance of <see cref="Values.GameField"/>.
		/// </summary>
		public required Values.GameField Field { get; init; }
	}
}
