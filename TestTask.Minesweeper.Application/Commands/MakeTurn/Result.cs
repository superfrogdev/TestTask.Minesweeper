namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a result of <see cref="Command"/>.
	/// </summary>
	public sealed class Result
	{
		/// <summary>
		/// Count of mines.
		/// </summary>
		public required ushort MinesCount { get; init; }

		/// <summary>
		/// Instance of <see cref="Domain.Enums.GameSessionStatus"/>.
		/// </summary>
		public required Domain.Enums.GameSessionStatus GameSessionStatus{ get; init; }

		/// <summary>
		/// Instance of <see cref="Domain.Values.GameField"/>.
		/// </summary>
		public required Domain.Values.GameField Field { get; init; }
	}
}
