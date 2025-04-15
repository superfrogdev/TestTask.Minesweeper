namespace TestTask.Minesweeper.Domain.Entities
{
	/// <summary>
	/// Represents a game session.
	/// </summary>
	public sealed class GameSession
	{
		/// <summary>
		/// Identifier of this instance.
		/// </summary>
		public required Guid Id { get; init; }

		/// <summary>
		/// Size of game field.
		/// </summary>
		/// <remarks>Measure unit - Cell's count.</remarks>
		public required Values.Size2d FieldSize { get; init; }

		/// <summary>
		/// Count of mines.
		/// </summary>
		/// <remarks>Less than <see cref="Values.Size2d.CalculateArea"/> from <see cref="FieldSize"/>.</remarks>
		public required ushort MinesCount { get; init; }

		/// <summary>
		/// Indicates if this instance is completed.
		/// </summary>
		public required bool IsCompleted { get; set; }

		/// <summary>
		/// Represents a collection of <see cref="Turn"/>, which were made during this instance.
		/// </summary>
		public required ICollection<Turn> Turns { get; set; } = new List<Turn>();

		/// <summary>
		/// Represents a collection of <see cref="Snapshot"/>, which were made during this instance.
		/// </summary>
		public required ICollection<Snapshot> Snapshots { get; set; } = new List<Snapshot>();
	}
}
