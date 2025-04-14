namespace TestTask.Minesweeper.Service.Api
{
	/// <summary>
	/// Represents an error description.
	/// </summary>
	public sealed class ErrorDescription
	{
		/// <summary>
		/// Error message.
		/// </summary>
		public required string Error { get; init; }
	}
}
