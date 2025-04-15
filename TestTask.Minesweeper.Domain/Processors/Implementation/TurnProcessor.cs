using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Domain.Processors.Implementation
{
	/// <summary>
	/// Represents an implementation of <see cref="ITurnProcessor"/>.
	/// </summary>
	internal sealed class TurnProcessor : ITurnProcessor
	{
		private readonly ISnapshotProcessor _snapshotProcessor;

		/// <summary>
		/// Initializes a new instance of <see cref="TurnProcessor"/>.
		/// </summary>
		/// <param name="snapshotProcessor">Instance of <see cref="ISnapshotProcessor"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="snapshotProcessor"/> cannot be <see langword="null"/>.</exception>
		public TurnProcessor(ISnapshotProcessor snapshotProcessor)
			: base()
		{
			_snapshotProcessor = snapshotProcessor ?? throw new ArgumentNullException(nameof(snapshotProcessor));
		}

		/// <inheritdoc/>
		public Enums.TurnResult Process(IOrderedEnumerable<Turn> turns, Snapshot lastSnapshot, out Snapshot newSnapshot)
		{
			ArgumentNullException.ThrowIfNull(turns, nameof(turns));

			ArgumentNullException.ThrowIfNull(lastSnapshot, nameof(lastSnapshot));

			Enums.TurnResult resultOfProcess;

			using (var turnsEnumerator = turns.GetEnumerator())
			{
				if (!turnsEnumerator.MoveNext())
				{
					throw new Exception("No turns!");
				}

				do
				{
					var currentTurn = turnsEnumerator.Current;

					newSnapshot = _snapshotProcessor.Process(lastSnapshot, currentTurn, out resultOfProcess);
				}
				while (turnsEnumerator.MoveNext());
			}

			return resultOfProcess;
		}
	}
}
