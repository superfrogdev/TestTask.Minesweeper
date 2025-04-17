using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Persistence.Configurations
{
	/// <summary>
	/// Represents a <see cref="IEntityTypeConfiguration{TEntity}"/> for <see cref="Turn"/>.
	/// </summary>
	public sealed class TurnConfiguration : IEntityTypeConfiguration<Turn>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<Turn> builder)
		{
			builder.HasKey(nameof(Turn.Number), "GameSessionId");

			builder.Property<Guid>("GameSessionId")
					.IsRequired()
					.HasComment("Identifier of game session.");

			builder.Property(propertyExpression => propertyExpression.Number)
					.IsRequired()
					.HasColumnName(nameof(Turn.Number))
					.HasComment("Number of turn.");

			builder.ComplexProperty(propertyExpression => propertyExpression.CellCoordinates
			, buildAction =>
			{
				buildAction.Property(propertyExpression => propertyExpression.X)
							.IsRequired()
							.HasComment("X-coordinate of target cell.");

				buildAction.Property(propertyExpression => propertyExpression.Y)
							.IsRequired()
							.HasComment("Y-coordinate of target cell.");
			});
		}
	}
}
