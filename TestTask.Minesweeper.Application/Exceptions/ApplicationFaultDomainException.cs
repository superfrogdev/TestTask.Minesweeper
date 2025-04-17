namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a <see cref="ApplicationFaultBusinessException"/> for <see cref="Domain.Exceptions.DomainException"/>.
	/// </summary>
	public sealed class ApplicationFaultDomainException : ApplicationFaultBusinessException
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ApplicationFaultDomainException"/>.
		/// </summary>
		/// <param name="exception">Instance of <see cref="Domain.Exceptions.DomainException"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="exception"/> cannot be <see langword="null"/>.</exception>
		public ApplicationFaultDomainException(Domain.Exceptions.DomainException exception)
			: base(exception != null ? exception.Message : throw new ArgumentNullException(nameof(exception)), exception)
		{ }
	}
}
