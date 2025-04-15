using System.Text.Json.Serialization;

namespace TestTask.Minesweeper.Service.Api
{
	/// <summary>
	/// Represents a new turn.
	/// </summary>
	public sealed class NewTurn
	{
		/// <summary>
		/// Identifier of game.
		/// </summary>
		[JsonPropertyName("game_id")]
		public required Guid GameId { get; init; }

		/// <summary>
		/// Index of column.
		/// </summary>
		[JsonPropertyName("col")]
		public required short Column { get; init; }

		/// <summary>
		/// Index of row.
		/// </summary>
		public required short Row { get; init; }
	}
}
