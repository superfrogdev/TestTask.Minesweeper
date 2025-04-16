namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a processor of <see cref="Entities.Turn"/>.
	/// </summary>
	public interface ITurnProcessor
	{
		/// <summary>
		/// Processes <paramref name="turns"/>, which were made since <paramref name="lastSnapshot"/>.
		/// </summary>
		/// <param name="turns">Enumeration of <see cref="Entities.Turn"/> to process.</param>
		/// <param name="lastSnapshot">Instance of last <see cref="Entities.Snapshot"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="turns"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="lastSnapshot"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="turns"/> must has at least one element.</exception>
		/// <returns>Result of processing <paramref name="turns"/>.</returns>
		(Enums.TurnResult LastTurnResult, ushort lastTurnProcessedCellCount, Values.GameField GameFieldAfterAll)
			Process(IOrderedEnumerable<Entities.Turn> turns, Entities.Snapshot lastSnapshot);
	}
}
