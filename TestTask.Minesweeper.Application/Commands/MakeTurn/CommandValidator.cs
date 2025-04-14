using FluentValidation;

namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a validator for <see cref="Command"/>.
	/// </summary>
	public sealed class CommandValidator : AbstractValidator<Command>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="CommandValidator"/>.
		/// </summary>
		public CommandValidator()
			: base()
		{
			RuleFor(expression => expression.Column)
				.LessThan<Command, ushort>(30)
				.WithMessage("Must be less than 30.");

			RuleFor(expression => expression.Row)
				.LessThan<Command, ushort>(30)
				.WithMessage("Must be less than 30.");
		}
	}
}
