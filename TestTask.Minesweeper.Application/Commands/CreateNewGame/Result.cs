namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a result of <see cref="Command"/>.
	/// </summary>
	public sealed class Result
	{
		/// <summary>
		/// Identifier of game.
		/// </summary>
		public required Guid GameId { get; init; }

		/// <summary>
		/// Game field.
		/// </summary>
		public required Domain.Values.GameField Field { get; init; }
	}
}
