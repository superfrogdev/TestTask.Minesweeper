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
			CreateMap<NewGameParameters, Application.Commands.CreateNewGame.Command>()
				.ForMember(destinationMember => destinationMember.FieldSize
							, memberOptions => memberOptions.MapFrom(sourceMember => new Domain.Values.Size2d(sourceMember.Width, sourceMember.Height)));

			CreateMap<(Application.Commands.CreateNewGame.Result Result, NewGameParameters NewGameParameters), GameState>()
				.ForMember(destinationMember => destinationMember.GameId, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.GameId))
				.ForMember(destinationMember => destinationMember.IsCompleted, memberOptions => memberOptions.MapFrom(sourceMember => false))
				.ForMember(destinationMember => destinationMember.Width, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.Width))
				.ForMember(destinationMember => destinationMember.Height, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.Height))
				.ForMember(destinationMember => destinationMember.MinesCount, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewGameParameters.MinesCount))
				.ForMember(destinationMember => destinationMember.Field, memberOptions => memberOptions.MapFrom(sourceMember => ConvertFrom(sourceMember.Result.Field, default)));

			CreateMap<NewTurn, Application.Commands.MakeTurn.Command>()
				.ForMember(destinationMember => destinationMember.SelectedCellCoordinates
							, memberOptions => memberOptions.MapFrom(sourceMember => new Domain.Values.Point2d(sourceMember.Column, sourceMember.Row)));

			CreateMap<(Application.Commands.MakeTurn.Result Result, NewTurn NewTurn), GameState>()
				.ForMember(destinationMember => destinationMember.GameId, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.NewTurn.GameId))
				.ForMember(destinationMember => destinationMember.IsCompleted, memberOptions => memberOptions.MapFrom(sourceMember => IsCompleted(sourceMember.Result.GameSessionStatus)))
				.ForMember(destinationMember => destinationMember.Width, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.Field.Size.Width))
				.ForMember(destinationMember => destinationMember.Height, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.Field.Size.Height))
				.ForMember(destinationMember => destinationMember.MinesCount, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Result.MinesCount))
				.ForMember(destinationMember => destinationMember.Field
							, memberOptions => memberOptions.MapFrom(sourceMember => ConvertFrom(sourceMember.Result.Field, sourceMember.Result.GameSessionStatus)));
		}

		private static FieldType[,] ConvertFrom(Domain.Values.GameField field, Domain.Enums.GameSessionStatus? gameSessionStatus)
		{
			var fieldTypes = new FieldType[field.Size.Height, field.Size.Width];

			if (gameSessionStatus.HasValue && IsCompleted(gameSessionStatus.Value))
			{
				var forMineCase = gameSessionStatus.Value == Domain.Enums.GameSessionStatus.PlayerWasDefeated ? FieldType.MineExploded : FieldType.Mine;

				for (var i = 0; i < fieldTypes.GetLength(0); i++)
				{
					for (var j = 0; j < fieldTypes.GetLength(1); j++)
					{
						var cell = field[j, i];

						if (cell.Value != Domain.Enums.CellValue.Mine)
						{
							fieldTypes[i, j] = (FieldType)(byte)cell.Value;
						}
						else
						{
							fieldTypes[i, j] = forMineCase;
						}
					}
				}
			}
			else
			{
				for (var i = 0; i < fieldTypes.GetLength(0); i++)
				{
					for (var j = 0; j < fieldTypes.GetLength(1); j++)
					{
						var cell = field[j, i];

						if (cell.IsOpened)
						{
							fieldTypes[i, j] = (FieldType)(byte)cell.Value;
						}
						else
						{
							fieldTypes[i, j] = FieldType.Hidden;
						}
					}
				}
			}

			return fieldTypes;
		}

		private static bool IsCompleted(Domain.Enums.GameSessionStatus status)
		{
			return status == Domain.Enums.GameSessionStatus.PlayerWasDefeated
					|| status == Domain.Enums.GameSessionStatus.PlayerWon;
		}
	}
}
