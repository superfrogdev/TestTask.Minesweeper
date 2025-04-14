using System.Text.Json.Serialization;

namespace TestTask.Minesweeper.Service.Api
{
	/// <summary>
	/// Represents a game state.
	/// </summary>
	public sealed class GameState
	{
		/// <summary>
		/// Identifier of game.
		/// </summary>
		[JsonPropertyName("game_id")]
		public required Guid GameId { get; init; }

		/// <summary>
		/// Width of game field.
		/// </summary>
		public required ushort Width { get; init; }

		/// <summary>
		/// Height of game field.
		/// </summary>
		public required ushort Height { get; init; }

		/// <summary>
		/// Count of mines.
		/// </summary>
		[JsonPropertyName("mines_count")]
		public required ushort MinesCount { get; init; }

		/// <summary>
		/// Indicates that game is completed.
		/// </summary>
		[JsonPropertyName("completed")]
		public required bool IsCompleted { get; init; }

		/// <summary>
		/// Game field.
		/// </summary>
		public required FieldType[,] Field { get; init; }
	}
}
