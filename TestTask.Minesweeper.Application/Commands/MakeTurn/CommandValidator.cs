using FluentValidation;

namespace TestTask.Minesweeper.Application.Commands.MakeTurn
{
	/// <summary>
	/// Represents a validator for <see cref="Command"/>.
	/// </summary>
	public sealed class CommandValidator : AbstractValidator<Command>
	{
		private static readonly Domain.Values.Rectangle _maxPossibleFieldRectangle = new(Domain.Values.Point2d.Zero, new Domain.Values.Size2d(30));

		/// <summary>
		/// Initializes a new instance of <see cref="CommandValidator"/>.
		/// </summary>
		public CommandValidator()
			: base()
		{
			RuleFor(expression => expression.SelectedCellCoordinates)
				.Must(_maxPossibleFieldRectangle.Contains)
				.WithMessage($@"Must be in rectangle: ""{_maxPossibleFieldRectangle}"".");
		}
	}
}
