using MediatR;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a command to create a new game.
	/// </summary>
	public sealed class Command : IRequest<Result>
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
		public required ushort MinesCount { get; init; }
	}
}
