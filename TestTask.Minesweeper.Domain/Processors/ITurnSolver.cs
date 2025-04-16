namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a solver of <see cref="Entities.Turn"/>.
	/// </summary>
	public interface ITurnSolver
	{
		/// <summary>
		/// Solves <paramref name="turn"/> and applies changes to <paramref name="gameField"/>.
		/// </summary>
		/// <param name="turn">Turn to solve.</param>
		/// <param name="gameField">Actual game field.</param>
		/// <param name="processedCellCount">Count of processed cells in <paramref name="gameField"/> after <paramref name="turn"/> was solved.</param>
		/// <exception cref="ArgumentNullException"><paramref name="turn"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="gameField"/> cannot be <see langword="null"/>.</exception>
		/// <returns>Result of solving <paramref name="turn"/>.</returns>
		Enums.TurnResult Solve(Entities.Turn turn, Values.GameField gameField, out ushort processedCellCount);
	}
}
