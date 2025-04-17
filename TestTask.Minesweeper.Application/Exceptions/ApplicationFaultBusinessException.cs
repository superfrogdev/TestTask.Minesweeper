namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a <see cref="ApplicationFaultException"/> for business exceptions.
	/// </summary>
	public abstract class ApplicationFaultBusinessException : ApplicationFaultException, IBusinessException
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationFaultBusinessException"/>.
		/// </summary>
		/// <param name="message"><inheritdoc cref="ApplicationFaultException.ApplicationFaultException(string?)" path="/param[@name='message']"/></param>
		protected ApplicationFaultBusinessException(string message)
			: base(message)
		{ }

		/// <summary>
		/// <inheritdoc cref="ApplicationFaultBusinessException.ApplicationFaultBusinessException(string)"/>
		/// </summary>
		/// <param name="message"><inheritdoc cref="ApplicationFaultException.ApplicationFaultException(string?, Exception?)" path="/param[@name='message']"/></param>
		/// <param name="innerException"><inheritdoc cref="ApplicationFaultException.ApplicationFaultException(string?, Exception?)" path="/param[@name='innerException']"/></param>
		protected ApplicationFaultBusinessException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
