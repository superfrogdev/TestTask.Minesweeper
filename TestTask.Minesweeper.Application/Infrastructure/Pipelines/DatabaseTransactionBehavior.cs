using MediatR;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Minesweeper.Application.Infrastructure.Pipelines
{
	/// <summary>
	/// Represents a <see cref="IPipelineBehavior{TRequest, TResponse}"/>, which provides support of database transaction.
	/// </summary>
	/// <typeparam name="TRequest">Type of request.</typeparam>
	/// <typeparam name="TResponse">Type of response.</typeparam>
	internal sealed class DatabaseTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly Persistence.GameDbContext _gameDbContext;

		/// <summary>
		/// Initializes a new instance of <see cref="DatabaseTransactionBehavior{TRequest, TResponse}"/>.
		/// </summary>
		/// <param name="validators">Instance of <see cref="Persistence.GameDbContext"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="gameDbContext"/> cannot be <see langword="null"/>.</exception>
		public DatabaseTransactionBehavior(Persistence.GameDbContext gameDbContext)
			: base()
		{
			_gameDbContext = gameDbContext ?? throw new ArgumentNullException(nameof(gameDbContext));
		}

		/// <inheritdoc/>
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			IDbContextTransaction? dbContextTransaction = null;

			if (_gameDbContext.Database.CurrentTransaction == null)
			{
				_gameDbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

				dbContextTransaction = await _gameDbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted, cancellationToken)
																	.ConfigureAwait(false);
				
				//TODO: Need possibility to set another isolation level! Also, we don't need such implemented class: it's made primitive only because it's test work.
			}

			try
			{
				var response = await next(cancellationToken)
										.ConfigureAwait(false);

				if (dbContextTransaction != null)
				{
					await dbContextTransaction.CommitAsync(cancellationToken)
												.ConfigureAwait(false);
				}

				return response;
			}
			finally
			{
				if (dbContextTransaction != null)
				{
					await dbContextTransaction.DisposeAsync()
												.ConfigureAwait(false);
				}
			}
		}
	}
}
