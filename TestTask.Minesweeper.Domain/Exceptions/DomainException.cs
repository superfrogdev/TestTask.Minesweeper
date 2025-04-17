namespace TestTask.Minesweeper.Domain.Exceptions
{
	/// <summary>
	/// Represents a base <see cref="Exception"/> for domain.
	/// </summary>
	public abstract class DomainException : Exception
	{
		/// <summary>
		/// Initializes a new instance of <see cref="DomainException"/>.
		/// </summary>
		/// <param name="message"><inheritdoc cref="Exception.Exception(string?)" path="/param[@name='message']"/></param>
		protected DomainException(string message)
			: base(message)
		{ }

		/// <summary>
		/// <inheritdoc cref="DomainException.DomainException(string)(string)"/>
		/// </summary>
		/// <param name="message"><inheritdoc cref="Exception.Exception(string?, Exception?)" path="/param[@name='message']"/></param>
		/// <param name="innerException"><inheritdoc cref="Exception.Exception(string?, Exception?)" path="/param[@name='innerException']"/></param>
		protected DomainException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
