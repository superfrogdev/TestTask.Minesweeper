namespace TestTask.Minesweeper.Domain.Enums
{
	/// <summary>
	/// Represents a result of <see cref="Entities.Turn"/>.
	/// </summary>
	public enum TurnResult
	{
		/// <summary>
		/// All safe cells are opened, player won.
		/// </summary>
		Victory,

		/// <summary>
		/// Cell with mine is opened, player lost.
		/// </summary>
		Defeat,

		/// <summary>
		/// Empty cell is opened.
		/// </summary>
		EmptyCellOpened,

		/// <summary>
		/// Cell with number is opened.
		/// </summary>
		CellWithNumberOpened
	}
}
