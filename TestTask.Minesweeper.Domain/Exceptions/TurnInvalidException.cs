namespace TestTask.Minesweeper.Domain.Exceptions
{
	/// <summary>
	/// Represents a <see cref="DomainException"/>, which caused by invalid <see cref="Entities.Turn"/>.
	/// </summary>
	public sealed class TurnInvalidException : DomainException
	{
		/// <summary>
		/// Represents a reason, why <see cref="Entities.Turn"/> is invalid.
		/// </summary>
		public enum Reason
		{
			/// <summary>
			/// Indicates, that target <see cref="Values.Cell"/> of this <see cref="Entities.Turn"/> is out of <see cref="Values.GameField"/>.
			/// </summary>
			TargetCellOutOfGameField,

			/// <summary>
			/// Indicates, that target <see cref="Values.Cell"/> of this <see cref="Entities.Turn"/> has been opened already by previous turns.
			/// </summary>
			TargetCellAlreadyOpened
		}

		private readonly Reason _reason;

		/// <summary>
		/// Initializes a new instance of <see cref="TurnInvalidException"/>.
		/// </summary>
		/// <param name="reason">See <see cref="ReasonOfException"/>.</param>
		public TurnInvalidException(Reason reason)
			: base(@$"Turn is invalid.")
		{
			_reason = reason;

			this.Data.Add(nameof(ReasonOfException), _reason);
		}

		/// <summary>
		/// Reason of this exception.
		/// </summary>
		public Reason ReasonOfException
		{
			get
			{
				return _reason;
			}
		}
	}
}
