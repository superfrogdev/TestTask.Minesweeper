namespace TestTask.Minesweeper.Domain.Entities
{
	/// <summary>
	/// Represents a turn in <see cref="Entities.GameSession"/>.
	/// </summary>
	public class Turn
	{
		/// <summary>
		/// Instance of <see cref="Entities.GameSession"/>.
		/// </summary>
		public required GameSession GameSession { get; set; } = default!;

		/// <summary>
		/// Number of this instance.
		/// </summary>
		public required ushort Number { get; init; }

		/// <summary>
		/// Coordinates of <see cref="Values.Cell"/>, which was selected by player.
		/// </summary>
		public virtual required Values.Point2d CellCoordinates { get; init; }
	}
}
