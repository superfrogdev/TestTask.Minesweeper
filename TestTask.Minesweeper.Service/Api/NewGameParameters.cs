using System.Text.Json.Serialization;

namespace TestTask.Minesweeper.Service.Api
{
	/// <summary>
	/// Represents parameters for new game.
	/// </summary>
	public sealed class NewGameParameters
	{
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
	}
}
