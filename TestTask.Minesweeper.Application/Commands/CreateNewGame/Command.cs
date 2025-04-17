using MediatR;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a command to create a new game.
	/// </summary>
	public sealed class Command : IRequest<Result>
	{
		/// <summary>
		/// Size of game field.
		/// </summary>
		public required Domain.Values.Size2d FieldSize { get; init; }

		/// <summary>
		/// Count of mines.
		/// </summary>
		public required ushort MinesCount { get; init; }
	}
}
