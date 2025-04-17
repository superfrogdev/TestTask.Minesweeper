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
		/// Coordinates of selected cell.
		/// </summary>
		public required Domain.Values.Point2d SelectedCellCoordinates { get; init; }
	}
}
