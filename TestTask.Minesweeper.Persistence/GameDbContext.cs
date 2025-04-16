using Microsoft.EntityFrameworkCore;

using TestTask.Minesweeper.Domain.Entities;

namespace TestTask.Minesweeper.Persistence
{
	/// <summary>
	/// Represents a <see cref="DbContext"/> for game.
	/// </summary>
	public sealed class GameDbContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of <see cref="GameDbContext"/>.
		/// </summary>
		/// <param name="options">Instance of <see cref="DbContextOptions{TContext}"/> for this instance.</param>
		/// <exception cref="ArgumentNullException"><paramref name="options"/> cannot be <see langword="null"/>.</exception>
		public GameDbContext(DbContextOptions<GameDbContext> options)
			: base(options)
		{ }

		/// <summary>
		/// Set of <see cref="GameSession"/>.
		/// </summary>
		public DbSet<GameSession> GameSessions
		{
			get
			{
				return Set<GameSession>();
			}
		}

		/// <summary>
		/// Set of <see cref="Snapshot"/>.
		/// </summary>
		public DbSet<Snapshot> Snapshots
		{
			get
			{
				return Set<Snapshot>();
			}
		}

		/// <summary>
		/// Set of <see cref="Turn"/>.
		/// </summary>
		public DbSet<Turn> Turns
		{
			get
			{
				return Set<Turn>();
			}
		}

		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

			modelBuilder.Entity<GameSession>()
						.ToTable("gameSessions");

			modelBuilder.Entity<Turn>()
						.ToTable("turns");

			modelBuilder.Entity<Snapshot>()
						.ToTable("snapshots");
		}
	}
}
