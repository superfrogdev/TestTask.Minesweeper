using MediatR;

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
		/// <exception cref="ArgumentNullException"><paramref name="services"/> cannot be <see langword="null"/>.</exception>
		/// <returns>Instance of <see cref="ServiceCollection"/>.</returns>
		public static IServiceCollection AddMinesweeperApplication(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services, nameof(services));

			services.AddTransient<Random>()
					.AddTransient<Domain.Processors.IGameFieldCreator, Domain.Processors.Implementation.GameFieldCreatorByRandom>();

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.RequestLoggingBehavior<,>))
					.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.ApplicationFaultExceptionProcessorBehavior<,>))
					.AddTransient(typeof(IPipelineBehavior<,>), typeof(Infrastructure.Pipelines.RequestValidationBehavior<,>));

			return services;
		}
	}
}
