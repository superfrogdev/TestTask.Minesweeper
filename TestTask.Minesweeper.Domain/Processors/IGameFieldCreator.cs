namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a creator of game field.
	/// </summary>
	public interface IGameFieldCreator
	{
		/// <summary>
		/// Creates a field of <see cref="Values.Cell"/> with specified <paramref name="fieldSize"/> and <paramref name="minesCount"/>.
		/// </summary>
		/// <param name="fieldSize">Size of field.</param>
		/// <param name="minesCount">Count of mines.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="fieldSize"/> has invalid area.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="minesCount"/> must be less than area of <paramref name="fieldSize"/>.</exception>
		/// <returns>Created field of <see cref="Values.Cell"/>.</returns>
		Values.Cell[,] Create(Values.Size2d fieldSize, ushort minesCount);
	}
}
