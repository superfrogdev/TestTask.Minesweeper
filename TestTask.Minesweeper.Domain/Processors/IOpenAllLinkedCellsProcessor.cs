namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a processor, which will open all linked cells.
	/// </summary>
	public interface IOpenAllLinkedCellsProcessor
	{
		/// <summary>
		/// Opens all cells, which are linked to cell with specified coordinates <paramref name="startPoint"/>.
		/// </summary>
		/// <param name="gameField">Instance of <see cref="Values.GameField"/>.</param>
		/// <param name="startPoint">Coordinates of starting cell.</param>
		/// <exception cref="ArgumentNullException"><paramref name="gameField"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="startPoint"/> is out of range <paramref name="gameField"/>.</exception>
		/// <returns>Number of opened cells.</returns>
		ushort Open(Values.GameField gameField, Values.Point2d startPoint);
	}
}
