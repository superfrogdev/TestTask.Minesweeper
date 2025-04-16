using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Minesweeper.Persistence
{
	/// <summary>
	/// Represents extensions for database.
	/// </summary>
	public static class DatabaseExtensions
	{
		/// <summary>
		/// Applies migrations to database.
		/// </summary>
		/// <param name="serviceProvider">Instance of <see cref="IServiceProvider"/>.</param>
		/// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
		/// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> cannot be <see langword="null"/>.</exception>
		public static Task ApplyDatabaseMigrationsAsync<TDbContext>(this IServiceProvider serviceProvider)
			where TDbContext : DbContext
		{
			ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

			return serviceProvider.InternalApplyDatabaseMigrationsAsync<TDbContext>();
		}

		private static async Task InternalApplyDatabaseMigrationsAsync<TDbContext>(this IServiceProvider serviceProvider)
			where TDbContext : DbContext
		{
			var serviceScope = serviceProvider.CreateAsyncScope();

			await using (serviceScope.ConfigureAwait(false))
			{
				var dbContext = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();

				await using (dbContext.ConfigureAwait(false))
				{
					await dbContext.Database.MigrateAsync()
											.ConfigureAwait(false);
				}
			}
		}
	}
}
