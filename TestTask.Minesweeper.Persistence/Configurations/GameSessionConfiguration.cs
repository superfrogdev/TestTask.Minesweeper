using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Persistence.Configurations
{
	/// <summary>
	/// Represents a <see cref="IEntityTypeConfiguration{TEntity}"/> for <see cref="GameSession"/>.
	/// </summary>
	public sealed class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<GameSession> builder)
		{
			builder.HasKey(keyExpression => keyExpression.Id);

			builder.Property(propertyExpression => propertyExpression.Id)
					.ValueGeneratedOnAdd()
					.IsRequired()
					.HasComment("Identifier");

			builder.Property(propertyExpression => propertyExpression.MinesCount)
					.IsRequired()
					.HasComment("Count of mines.");

			builder.Property(propertyExpression => propertyExpression.Status)
					.IsRequired()
					.HasDefaultValue(Domain.Enums.GameSessionStatus.NotCompleted)
					.HasComment("Status.");

			builder.ComplexProperty(propertyExpression => propertyExpression.FieldSize
			, buildAction =>
				{
					buildAction.Property(propertyExpression => propertyExpression.Width)
								.IsRequired()
								.HasComment("Width of game field.");

					buildAction.Property(propertyExpression => propertyExpression.Height)
								.IsRequired()
								.HasComment("Height of game field.");
				});
		}
	}
}
