namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a processor of <see cref="Entities.Snapshot"/>.
	/// </summary>
	public interface ISnapshotProcessor
	{
		/// <summary>
		/// Process changes to <paramref name="previousSnapshot"/> from <paramref name="newTurn"/>.
		/// </summary>
		/// <param name="previousSnapshot"><see cref="Entities.Snapshot"/> to process.</param>
		/// <param name="newTurn">Instance of <see cref="Entities.Turn"/>.</param>
		/// <param name="newTurnResult">Result of new <see cref="Entities.Turn"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="previousSnapshot"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="newTurn"/> cannot be <see langword="null"/>.</exception>
		/// <returns>New <see cref="Entities.Snapshot"/>.</returns>
		Entities.Snapshot Process(Entities.Snapshot previousSnapshot, Entities.Turn newTurn, out Enums.TurnResult newTurnResult);
	}
}
