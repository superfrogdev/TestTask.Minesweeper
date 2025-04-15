namespace TestTask.Minesweeper.Domain.Entities
{
	/// <summary>
	/// Represents a turn in <see cref="Entities.GameSession"/>.
	/// </summary>
	public sealed class Turn
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
		/// Number of this instance.
		/// </summary>
		public required ushort Number { get; init; }

		/// <summary>
		/// Coordinates of <see cref="Values.Cell"/>, which was selected by player.
		/// </summary>
		public required Values.Point2d CellCoordinates { get; init; }
	}
}
