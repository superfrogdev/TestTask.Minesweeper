using AutoMapper;

namespace TestTask.Minesweeper.Service.Api.Mappings
{
	/// <summary>
	/// Represents a profile for mapping api.
	/// </summary>
	internal sealed class ApiProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ApiProfile"/>.
		/// </summary>
		public ApiProfile()
			: base()
		{
			CreateMap<NewGameParameters, Application.Commands.CreateNewGame.Command>();

			CreateMap<(Application.Commands.CreateNewGame.Result Result, NewGameParameters NewGameParameters), GameState>()
				.ForMember(destinationMember => destinationMember.GameId, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.GameId))
				.ForMember(destinationMember => destinationMember.Width, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.Width))
				.ForMember(destinationMember => destinationMember.Height, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.Height))
				.ForMember(destinationMember => destinationMember.MinesCount, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.MinesCount))
				.ForMember(destinationMember => destinationMember.Field, memberOptions => memberOptions.MapFrom(sourceMember => ConvertFrom(sourceMember.Result.Field)));

			CreateMap<NewTurn, Application.Commands.MakeTurn.Command>();

			CreateMap<(Application.Commands.MakeTurn.Result Result, NewTurn NewTurn), GameState>()
				.ForMember(destinationMember => destinationMember.GameId, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewTurn.GameId))
				.ForMember(destinationMember => destinationMember.Width, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.Width))
				.ForMember(destinationMember => destinationMember.Height, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.Height))
				.ForMember(destinationMember => destinationMember.MinesCount, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.MinesCount))
				.ForMember(destinationMember => destinationMember.Field, memberOptions => memberOptions.MapFrom(sourceMember => ConvertFrom(sourceMember.Result.Field)));
		}

		private static FieldType[,] ConvertFrom(byte[,] field)
		{
			var fieldTypes = new FieldType[field.GetLength(0), field.GetLength(1)];

			Buffer.BlockCopy(field, 0, fieldTypes, 0, field.Length);

			return fieldTypes;
		}
	}
}
