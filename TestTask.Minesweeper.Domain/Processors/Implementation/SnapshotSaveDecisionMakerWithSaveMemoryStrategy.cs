using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ISnapshotSaveDecisionMaker"/> with save memory strategy.
	/// </summary>
	public sealed class SnapshotSaveDecisionMakerWithSaveMemoryStrategy : ISnapshotSaveDecisionMaker
	{
		/// <inheritdoc/>
		public bool IsNeedToBeSaved(Snapshot previous, Snapshot actual, ITurnProcessor.ResultOfProcess resultOfProcess)
		{
			ArgumentNullException.ThrowIfNull(previous, nameof(previous));

			ArgumentNullException.ThrowIfNull(actual, nameof(actual));

			return resultOfProcess == ITurnProcessor.ResultOfProcess.Defeat
					|| resultOfProcess == ITurnProcessor.ResultOfProcess.Victory;
		}
	}
}
