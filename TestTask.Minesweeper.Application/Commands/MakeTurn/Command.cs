using MediatR;

namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a command to make next turn in game.
	/// </summary>
	public sealed class Command : IRequest<Result>
	{
		/// <summary>
		/// Identifier of game.
		/// </summary>
		public required Guid GameId { get; init; }

		/// <summary>
		/// Index of column.
		/// </summary>
		public required ushort Column { get; init; }

		/// <summary>
		/// Index of row.
		/// </summary>
		public required ushort Row { get; init; }
	}
}
