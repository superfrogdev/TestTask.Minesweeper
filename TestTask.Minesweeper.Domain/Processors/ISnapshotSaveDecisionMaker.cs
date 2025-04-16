namespace TestTask.Minesweeper.Domain.Processors
{
	/// <summary>
	/// Represents a decision maker in question save or not <see cref="Entities.Snapshot"/>.
	/// </summary>
	public interface ISnapshotSaveDecisionMaker
	{
		/// <summary>
		/// Checks if necessary to save changes after <see cref="Entities.Turn"/>.
		/// </summary>
		/// <param name="resultOfProcess">Result of turn.</param>
		/// <param name="countOfProcessedCells">Amount of processed cells after turn's solving.</param>
		/// <returns><see langword="true"/> - yes; otherwise - no.</returns>
		bool IsNeedToBeSaved(Enums.TurnResult resultOfProcess, ushort countOfProcessedCells);
	}
}
