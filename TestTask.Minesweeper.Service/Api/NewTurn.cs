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
		/// Number of column.
		/// </summary>
		[JsonPropertyName("col")]
		public required ushort Column { get; init; }

		/// <summary>
		/// Number of row.
		/// </summary>
		public required ushort Row { get; init; }
	}
}
