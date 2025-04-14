using System.Text;

using FluentValidation.Results;

namespace TestTask.Minesweeper.Application.Exceptions
{
	/// <summary>
	/// Represents a <see cref="ApplicationFaultException"/>, which is caused by validation failures.
	/// </summary>
	public sealed class ValidationFaultException : ApplicationFaultException, IBusinessException
	{
		private readonly IReadOnlyCollection<ValidationFailure> _validationFailures;

		/// <summary>
		/// Initializes a new instance of <see cref="ValidationFaultException"/>.
		/// </summary>
		/// <param name="validationFailures">See <see cref="ValidationFailures"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="validationFailures"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="validationFailures"/> must be has at least one element.</exception>
		public ValidationFaultException(IReadOnlyCollection<ValidationFailure> validationFailures)
			: base(CreateMessageFrom(validationFailures))
		{
			_validationFailures = validationFailures;
		}

		/// <summary>
		/// Collection of <see cref="ValidationFailure"/>.
		/// </summary>
		public IReadOnlyCollection<ValidationFailure> ValidationFailures
		{
			get
			{
				return _validationFailures;
			}
		}

		private static string CreateMessageFrom(IReadOnlyCollection<ValidationFailure> validationFailures)
		{
			ArgumentNullException.ThrowIfNull(validationFailures, nameof(validationFailures));

			if (validationFailures.Count == 0)
			{
				throw new ArgumentException("Must be at least one failure.", nameof(validationFailures));
			}

			var stringBuilder = new StringBuilder(validationFailures.Count * 50);

			foreach (var current in validationFailures)
			{
				stringBuilder.Append(current.PropertyName)
							 .Append(" - ")
							 .AppendLine(current.ErrorMessage);
			}

			return stringBuilder.ToString();
		}
	}
}
