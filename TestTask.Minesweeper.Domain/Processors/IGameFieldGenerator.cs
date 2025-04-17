namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a generator of game field.
	/// </summary>
	public interface IGameFieldGenerator
	{
		/// <summary>
		/// Generates a data to <paramref name="gameField"/> with specified <paramref name="minesCount"/>.
		/// </summary>
		/// <param name="gameField">Instance of <see cref="Values.GameField"/>.</param>
		/// <param name="minesCount">Count of mines.</param>
		/// <exception cref="ArgumentNullException"><paramref name="gameField"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="minesCount"/> must be less than area of <paramref name="gameField"/>.</exception>
		void Generate(Values.GameField gameField, ushort minesCount);
	}
}
