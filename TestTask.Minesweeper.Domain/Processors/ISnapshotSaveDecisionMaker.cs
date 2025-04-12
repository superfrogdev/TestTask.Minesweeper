namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a decision maker in question save or not <see cref="Entities.Snapshot"/>.
	/// </summary>
	public interface ISnapshotSaveDecisionMaker
	{
		/// <summary>
		/// Checks if necessary to save <paramref name="actual"/>.
		/// </summary>
		/// <param name="previous">Previous <see cref="Entities.Snapshot"/>.</param>
		/// <param name="actual">Actual <see cref="Entities.Snapshot"/>.</param>
		/// <param name="resultOfProcess">Result of turn, which create <paramref name="actual"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="previous"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="actual"/> cannot be <see langword="null"/>.</exception>
		/// <returns><see langword="true"/> - yes; otherwise - no.</returns>
		bool IsNeedToBeSaved(Entities.Snapshot previous, Entities.Snapshot actual, ITurnProcessor.ResultOfProcess resultOfProcess);
	}
}
