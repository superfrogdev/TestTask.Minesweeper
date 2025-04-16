namespace TestTask.Minesweeper.Domain.Enums
{
	/// <summary>
	/// Represents a status of <see cref="Entities.GameSession"/>.
	/// </summary>
	public enum GameSessionStatus : byte
	{
		/// <summary>
		/// Game session is not completed.
		/// </summary>
		NotCompleted,

		/// <summary>
		/// Game session was completed and player was defeated.
		/// </summary>
		PlayerWasDefeated,

		/// <summary>
		/// Game session was completed and player won.
		/// </summary>
		PlayerWon
	}
}
