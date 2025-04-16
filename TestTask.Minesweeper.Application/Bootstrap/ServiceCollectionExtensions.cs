using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Minesweeper.Application.Bootstrap
{
	/// <summary>
	/// Represents extensions for <see cref="IServiceCollection"/>.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds required dependencies for minesweeper application to <paramref name="services"/>.
		/// </summary>
		/// <param name="services">Instance of <see cref="ServiceCollection"/>.</param>
		/// <param name="configuration">Instance of <see cref="IConfiguration"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="services"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="configuration"/> cannot be <see langword="null"/>.</exception>
		/// <returns>Instance of <see cref="ServiceCollection"/>.</returns>
		public static IServiceCollection AddMinesweeperApplication(this IServiceCollection services, IConfiguration configuration)
		{
			ArgumentNullException.ThrowIfNull(services, nameof(services));

			ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

			services.AddTransient<Random>()
					.AddTransient<Domain.Processors.IGameFieldGenerator, Domain.Processors.Implementation.GameFieldGeneratorByRandom>()
					.AddSingleton<Domain.Processors.IOpenAllLinkedCellsProcessor, Domain.Processors.Implementation.OpenAllLinkedCellsProcessorThroughBreadthFirstSearch>()
					.AddSingleton<Domain.Processors.ITurnSolver, Domain.Processors.Implementation.TurnSolver>()
					.AddSingleton<Domain.Processors.ISnapshotSaveDecisionMaker, Domain.Processors.Implementation.SnapshotSaveDecisionMakerWithSaveOnLargeChanges>()
					.AddSingleton<Domain.Processors.ITurnProcessor, Domain.Processors.Implementation.TurnProcessor>();

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.RequestLoggingBehavior<,>))
					.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.ApplicationFaultExceptionProcessorBehavior<,>))
					.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.RequestValidationBehavior<,>))
					.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.DatabaseTransactionBehavior<,>));

			services.AddDbContext<Persistence.GameDbContext>(SetupContext);

			void SetupContext(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
			{
				var connectionString = configuration.GetConnectionString("PostgresDb");

				builder.UseNpgsql(connectionString);

				var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

				if (!string.Equals(environment, "PRODUCTION", StringComparison.OrdinalIgnoreCase))
				{
					builder.EnableSensitiveDataLogging();
				}
			}

			return services;
		}
	}
}
