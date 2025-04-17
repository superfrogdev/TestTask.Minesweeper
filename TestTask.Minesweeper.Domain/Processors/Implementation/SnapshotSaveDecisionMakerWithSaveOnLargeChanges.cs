namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ISnapshotSaveDecisionMaker"/> with save on large changes.
	/// </summary>
	public sealed class SnapshotSaveDecisionMakerWithSaveOnLargeChanges : ISnapshotSaveDecisionMaker
	{
		/// <inheritdoc/>
		public bool IsNeedToBeSaved(Enums.TurnResult resultOfProcess, ushort countOfProcessedCells)
		{
			return resultOfProcess == Enums.TurnResult.Defeat
					|| resultOfProcess == Enums.TurnResult.Victory
					|| (resultOfProcess == Enums.TurnResult.EmptyCellOpened
							&& countOfProcessedCells > 40);
		}
	}
}
