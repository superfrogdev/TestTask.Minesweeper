namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a base <see cref="Exception"/> for application.
	/// </summary>
	public abstract class ApplicationFaultException : Exception
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationFaultException"/>.
		/// </summary>
		/// <param name="message"><inheritdoc cref="Exception.Exception(string?)" path="/param[@name='message']"/></param>
		protected ApplicationFaultException(string message)
			: base(message)
		{ }

		/// <summary>
		/// <inheritdoc cref="ApplicationFaultException.ApplicationFaultException(string)"/>
		/// </summary>
		/// <param name="message"><inheritdoc cref="Exception.Exception(string?, Exception?)" path="/param[@name='message']"/></param>
		/// <param name="innerException"><inheritdoc cref="Exception.Exception(string?, Exception?)" path="/param[@name='innerException']"/></param>
		protected ApplicationFaultException(string message, Exception innerException)
			: base(message, innerException)
		{ }

	}
}
