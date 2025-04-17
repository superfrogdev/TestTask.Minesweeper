using System.Text.Json;
using System.Text.Json.Serialization;

using TestTask.Minesweeper.Service.Swagger;

namespace TestTask.Minesweeper.Service.Api.Json
{
	/// <summary>
	/// Represents a <see cref="JsonConverter{T}"/>, where T is two-dimensional array of <see cref="FieldType"/>.
	/// </summary>
	internal sealed class FieldJsonConverter : JsonConverter<FieldType[,]>
	{
		//For small number of iterations linear search is ok.
		private static readonly IReadOnlyCollection<KeyValuePair<FieldType, string>> _relationBetweenFieldTypeValuesAndDescriptions;

		/// <summary>
		/// Initializes static data.
		/// </summary>
		static FieldJsonConverter()
		{
			var enumValues = Enum.GetValues<FieldType>();

			var relationsBetweenValueAndDisplay = new KeyValuePair<FieldType, string>[enumValues.Length];

			for (var i = 0; i < enumValues.Length; i++)
			{
				var value = enumValues[i];

				relationsBetweenValueAndDisplay[i] = new KeyValuePair<FieldType, string>(value, value.GetDisplayName());
			}

			_relationBetweenFieldTypeValuesAndDescriptions = relationsBetweenValueAndDisplay;
		}

		/// <inheritdoc/>
		public override FieldType[,]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void Write(Utf8JsonWriter writer, FieldType[,] value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();

			for (var i = 0; i < value.GetLength(0); i++)
			{
				writer.WriteStartArray();

				for (var j = 0; j < value.GetLength(1); j++)
				{
					WriteValue(value[i, j], writer);
				}

				writer.WriteEndArray();
			}

			writer.WriteEndArray();

			static void WriteValue(Api.FieldType value, Utf8JsonWriter jsonWriter)
			{
				foreach (var current in _relationBetweenFieldTypeValuesAndDescriptions)
				{
					if (current.Key == value)
					{
						jsonWriter.WriteStringValue(current.Value);

						break;
					}
				}
			}
		}
	}
}
