using FluentValidation;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
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
			RuleFor(expression => expression.Width)
				.LessThanOrEqualTo<Command, ushort>(30)
				.WithMessage("Must be equal or less than 30.")
				.GreaterThan<Command, ushort>(0)
				.WithMessage("Must be greater than 0.");

			RuleFor(expression => expression.Height)
				.LessThanOrEqualTo<Command, ushort>(30)
				.WithMessage("Must be equal or less than 30.")
				.GreaterThan<Command, ushort>(0)
				.WithMessage("Must be greater than 0.");

			RuleFor(expression => expression.MinesCount)
				.Must((command, minesCount) => minesCount < command.Width * command.Height)
				.WithMessage("Must be less than game field area.");
		}
	}
}
