namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a common <see cref="ApplicationFaultException"/>.
	/// </summary>
	public sealed class ApplicationFaultCommonException : ApplicationFaultException
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationFaultCommonException"/>.
		/// </summary>
		/// <param name="innerException">Instance of <see cref="Exception"/> that cause of current exception.</param>
		/// <exception cref="ArgumentNullException"><paramref name="innerException"/> cannot be <see langword="null"/>.</exception>
		public ApplicationFaultCommonException(Exception innerException)
			: base(@$"Common trouble: ""{(innerException != null ? innerException.Message : throw new ArgumentNullException(nameof(innerException)))}"".", innerException)
		{ }

	}
}
