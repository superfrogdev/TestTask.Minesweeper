using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// 
	/// </summary>
	internal sealed class TurnProcessor : ITurnProcessor
	{
		private readonly ISnapshotProcessor _snapshotProcessor;

		public TurnProcessor(ISnapshotProcessor snapshotProcessor)
			: base()
		{
			_snapshotProcessor = snapshotProcessor ?? throw new ArgumentNullException(nameof(snapshotProcessor));
		}

		/// <inheritdoc/>
		public ITurnProcessor.ResultOfProcess Process(IOrderedEnumerable<Turn> turns, Snapshot lastSnapshot, out Snapshot newSnapshot)
		{
			ArgumentNullException.ThrowIfNull(turns, nameof(turns));

			ArgumentNullException.ThrowIfNull(lastSnapshot, nameof(lastSnapshot));

			ITurnProcessor.ResultOfProcess resultOfProcess = default;

			newSnapshot = default;

			var hasAnyTurn = false;

			foreach (var currentTurn in turns)
			{
				hasAnyTurn = true;

				newSnapshot = _snapshotProcessor.Process(lastSnapshot, currentTurn, out resultOfProcess);
			}

			if (!hasAnyTurn)
			{
				throw new Exception("No turns!");
			}

			return resultOfProcess;
		}
	}
}
