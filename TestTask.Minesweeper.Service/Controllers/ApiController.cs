using System.ComponentModel.DataAnnotations;
using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace TestTask.Minesweeper.Service.Controllers
{
	/// <summary>
	/// Represents a minesweeper api.
	/// </summary>
	[ApiController]
	[Route("api")]
	public sealed class ApiController : ControllerBase
	{
		private readonly ILogger<ApiController> _logger;

		/// <summary>
		/// Initializes a new instance of <see cref="ApiController"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for <see cref="ApiController"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		public ApiController(ILogger<ApiController> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Creates a new game with specified <paramref name="parameters"/>.
		/// </summary>
		/// <param name="parameters">Parameters of new game.</param>
		/// <param name="cancellationToken">Token for cancellation.</param>
		/// <returns>State of new game.</returns>
		[HttpPost("/new")]
		[ProducesResponseType(typeof(Api.GameState), (int)HttpStatusCode.OK, "application/json")]
		[ProducesResponseType(typeof(Api.ErrorDescription), (int)HttpStatusCode.BadRequest, "application/json")]
		[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError, "application/json")]
		public Task<ActionResult<Api.GameState>> NewGame([Required, FromBody] Api.NewGameParameters parameters, CancellationToken cancellationToken)
		{
			var stub = new Api.FieldType[,] { { Api.FieldType.Empty, Api.FieldType.Two }, { Api.FieldType.Mine, Api.FieldType.One } };

			return Task.FromResult(new ActionResult<Api.GameState>(
				new Api.GameState() { GameId = Guid.NewGuid(), Height = 1, Width = 1, IsCompleted = false, MinesCount = 0, Field = stub }));
		}

		/// <summary>
		/// Makes a new turn in game with specified identifier.
		/// </summary>
		/// <param name="newTurn">Parameters of new turn.</param>
		/// <param name="cancellationToken">Token for cancellation.</param>
		/// <returns>State of game after turn.</returns>
		[HttpPost("/turn")]
		[ProducesResponseType(typeof(Api.GameState), (int)HttpStatusCode.OK, "application/json")]
		[ProducesResponseType(typeof(Api.ErrorDescription), (int)HttpStatusCode.BadRequest, "application/json")]
		[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError, "application/json")]
		public Task<ActionResult<Api.GameState>> NewTurn([Required, FromBody] Api.NewTurn newTurn, CancellationToken cancellationToken)
		{
			var stub = new Api.FieldType[,] { { Api.FieldType.Empty, Api.FieldType.Two }, { Api.FieldType.Mine, Api.FieldType.One } };

			return Task.FromResult(new ActionResult<Api.GameState>(new Api.GameState() { GameId = Guid.NewGuid(), Height = 1, Width = 1, IsCompleted = true, MinesCount = 0, Field = stub }));
		}
	}
}
