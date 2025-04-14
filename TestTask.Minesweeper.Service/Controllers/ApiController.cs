using System.ComponentModel.DataAnnotations;
using System.Net;

using AutoMapper;

using MediatR;

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
		private readonly ISender _sender;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of <see cref="ApiController"/>.
		/// </summary>
		/// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/> for <see cref="ApiController"/>.</param>
		/// <param name="sender">Instance of <see cref="ISender"/>.</param>
		/// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="logger"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sender"/> cannot be <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="mapper"/> cannot be <see langword="null"/>.</exception>
		public ApiController(ILogger<ApiController> logger, ISender sender, IMapper mapper)
			: base()
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_sender = sender ?? throw new ArgumentNullException(nameof(sender));

			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
		public async Task<ActionResult<Api.GameState>> NewGame([Required, FromBody] Api.NewGameParameters parameters, CancellationToken cancellationToken)
		{
			var command = _mapper.Map<Api.NewGameParameters, Application.Commands.CreateNewGame.Command>(parameters);

			var result = await _sender.Send(command, cancellationToken);
			
			var response = _mapper.Map<(Application.Commands.CreateNewGame.Result, Api.NewGameParameters), Api.GameState>((result, parameters));

			return new ActionResult<Api.GameState>(response);
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
		public async Task<ActionResult<Api.GameState>> NewTurn([Required, FromBody] Api.NewTurn newTurn, CancellationToken cancellationToken)
		{
			var command = _mapper.Map<Api.NewTurn, Application.Commands.MakeTurn.Command>(newTurn);

			var result = await _sender.Send(command, cancellationToken);

			var response = _mapper.Map<(Application.Commands.MakeTurn.Result, Api.NewTurn), Api.GameState>((result, newTurn));

			return new ActionResult<Api.GameState>(response);
		}
	}
}
