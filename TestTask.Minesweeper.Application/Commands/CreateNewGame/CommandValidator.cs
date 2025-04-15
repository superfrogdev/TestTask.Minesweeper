using FluentValidation;

namespace TestTask.Minesweeper.Application.Commands.CreateNewGame
{
	/// <summary>
	/// Represents a validator for <see cref="Command"/>.
	/// </summary>
	public sealed class CommandValidator : AbstractValidator<Command>
	{
		private static readonly Domain.Values.Size2d _maxPossibleFieldSize = new(30);

		/// <summary>
		/// Initializes a new instance of <see cref="CommandValidator"/>.
		/// </summary>
		public CommandValidator()
			: base()
		{
			RuleFor(expression => expression.FieldSize)
				.Must(fieldSize => fieldSize.Width <= _maxPossibleFieldSize.Width && fieldSize.Height <= _maxPossibleFieldSize.Height)
				.WithMessage(@$"Must be equal or less than ""{_maxPossibleFieldSize}"".");

			RuleFor(expression => expression.MinesCount)
				.Must((command, minesCount) => minesCount < command.FieldSize.CalculateArea())
				.WithMessage("Must be less than game field area.");
		}
	}
}
