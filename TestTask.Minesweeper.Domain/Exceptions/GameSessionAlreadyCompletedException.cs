namespace TestTask.Minesweeper.Domain.Exceptions
{
	/// <summary>
	/// Represents a <see cref="DomainException"/>, when <see cref="Entities.GameSession"/> has been completed already.
	/// </summary>
	public sealed class GameSessionAlreadyCompletedException : DomainException
	{
		private readonly Guid _id;

		/// <summary>
		/// Initializes a new instance of <see cref="GameSessionAlreadyCompletedException"/>.
		/// </summary>
		/// <param name="id">See <see cref="GameSessionId"/>.</param>
		public GameSessionAlreadyCompletedException(Guid id)
			: base($@"Game session has been completed already.")
		{
			_id = id;

			this.Data.Add(nameof(GameSessionId), id);
		}

		/// <summary>
		/// Identifier of <see cref="Entities.GameSession"/>.
		/// </summary>
		public Guid GameSessionId
		{
			get
			{
				return _id;
			}
		}
	}
}
 