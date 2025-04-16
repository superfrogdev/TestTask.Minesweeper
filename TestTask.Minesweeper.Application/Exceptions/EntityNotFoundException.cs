namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a <see cref="ApplicationFaultBusinessException"/>, when business entity is not found.
	/// </summary>
	public sealed class EntityNotFoundException : ApplicationFaultBusinessException
	{
		/// <summary>
		/// Initializes a new instance of <see cref="EntityNotFoundException"/>.
		/// </summary>
		public EntityNotFoundException()
			: base("Entity is not found.")
		{ }
	}
}