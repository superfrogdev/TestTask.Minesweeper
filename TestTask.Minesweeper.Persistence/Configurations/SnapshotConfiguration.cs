using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestTask.Minesweeper.Domain.Entities;
using TestTask.Minesweeper.Domain.Values;

namespace TestTask.Minesweeper.Persistence.Configurations
{
	/// <summary>
	/// Represents a <see cref="IEntityTypeConfiguration{TEntity}"/> for <see cref="Snapshot"/>.
	/// </summary>
	public sealed class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<Snapshot> builder)
		{
			builder.Property<Guid>("GameSessionId")
					.IsRequired()
					.HasComment("Identifier of game session.");

			builder.Property<ushort?>("TurnNumber")
					.IsRequired(false)
					.HasComment("Number of turn.");

			builder.HasIndex("GameSessionId", "TurnNumber")
					.IsUnique()
					.AreNullsDistinct(false);

			builder.Property<Guid>("Id")
					.ValueGeneratedOnAdd()
					.IsRequired()
					.HasComment("Identifier of snapshot.");

			builder.HasKey("Id");

			builder.Property(propertyExpression => propertyExpression.Field)
					.IsRequired()
					.HasComment("Game field")
					.HasConversion<byte[]>(convertToProviderExpression => ToRawBytes(convertToProviderExpression), convertFromProviderExpression => ToGameField(convertFromProviderExpression));

			builder.HasOne(navigationExpression => navigationExpression.Turn)
				   .WithOne()
				   .HasForeignKey<Snapshot>("GameSessionId", "TurnNumber")
				   .HasPrincipalKey<Turn>("GameSessionId", nameof(Turn.Number))
				   .OnDelete(DeleteBehavior.Cascade);
		}

		private static byte[] ToRawBytes(GameField gameField)
		{
			var bytes = new byte[gameField.Count + 4];

			var bytesAsSpan = bytes.AsSpan();

			Span<byte> sizeValueAsBytes = stackalloc byte[2];

			var _ = BitConverter.TryWriteBytes(sizeValueAsBytes, gameField.Size.Width);

			sizeValueAsBytes.CopyTo(bytesAsSpan);

			_ = BitConverter.TryWriteBytes(sizeValueAsBytes, gameField.Size.Height);

			sizeValueAsBytes.CopyTo(bytesAsSpan.Slice(2));

			var tempBytes = bytesAsSpan.Slice(4);

			for (var i = 0; i < gameField.Count; i++)
			{
				tempBytes[i] = (byte)gameField[i];
			}

			return bytes;
		}

		private static GameField ToGameField(byte[] bytes)
		{
			var bytesAsSpan = bytes.AsSpan();

			var fieldSize = new Size2d(BitConverter.ToUInt16(bytesAsSpan.Slice(0, 2)), BitConverter.ToUInt16(bytesAsSpan.Slice(2, 2)));

			var gameField = new GameField(fieldSize);

			var tempBytes = bytesAsSpan.Slice(4);

			for (var i = 0; i < gameField.Count; i++)
			{
				gameField[i] = (Cell)tempBytes[i];
			}

			return gameField;
		}
	}
}
