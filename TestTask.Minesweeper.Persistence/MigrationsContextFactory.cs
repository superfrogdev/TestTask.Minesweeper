using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestTask.Minesweeper.Persistence
{
	/// <summary>
	/// Represents an implementation of <see cref="IDesignTimeDbContextFactory{TContext}"/> for <see cref="GameDbContext"/>.
	/// </summary>
	public sealed class MigrationsContextFactory : IDesignTimeDbContextFactory<GameDbContext>
	{
		/// <inheritdoc/>
		public GameDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();

			optionsBuilder.UseNpgsql(npgsqlOptionsAction => npgsqlOptionsAction.MigrationsAssembly("TestTask.Minesweeper.Persistence"));

			return new GameDbContext(optionsBuilder.Options);
		}
	}
}
