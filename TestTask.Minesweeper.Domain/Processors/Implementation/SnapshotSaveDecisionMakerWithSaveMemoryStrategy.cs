namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ISnapshotSaveDecisionMaker"/> with save memory strategy.
	/// </summary>
	public sealed class SnapshotSaveDecisionMakerWithSaveMemoryStrategy : ISnapshotSaveDecisionMaker
	{
		/// <inheritdoc/>
		public bool IsNeedToBeSaved(Enums.TurnResult resultOfProcess, ushort countOfProcessedCells)
		{
			return resultOfProcess == Enums.TurnResult.Defeat
					|| resultOfProcess == Enums.TurnResult.Victory;
		}
	}
}
