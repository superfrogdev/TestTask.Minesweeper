using Microsoft.OpenApi.Models;
using System.Text;
using System.Xml.Linq;

using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;

namespace TestTask.Minesweeper.Service.Swagger
{
	/// <summary>
	/// Represents a filter for <see cref="Enum"/>.
	/// </summary>
	internal sealed class EnumTypesSchemaFilter : ISchemaFilter
	{
		/// <summary>
		/// Instance of <see cref="XDocument"/>.
		/// </summary>
		private readonly XDocument? _xmlComments;

		/// <summary>
		/// Initializes a new instance of <see cref="EnumTypesSchemaFilter"/>.
		/// </summary>
		/// <param name="xmlPath">Path to documentation.</param>
		public EnumTypesSchemaFilter(string xmlPath)
		{
			_xmlComments = XDocument.Load(xmlPath);
		}

		/// <inheritdoc/>
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			//TODO: Improve code quality (more accurate work with strings) + probably, has sense to add possibility to add data for inherit.

			if (_xmlComments == null)
			{
				return;
			}

			if (schema.Enum != null
				&& schema.Enum.Count > 0
				&& context.Type != null
				&& context.Type.IsEnum)
			{
				var descriptionBuilder = schema.Description != null
											? new StringBuilder(schema.Description, (schema.Description.Length * 3 >> 1) + 1)
											: new StringBuilder();

				descriptionBuilder.Append("<p>Contains values:</p><ul>");

				var fullTypeName = context.Type.FullName;

				schema.Enum.Clear();
				
				foreach (var enumMemberName in Enum.GetValues(context.Type))
				{
					var enumMemberValue = ((System.Enum)enumMemberName).GetDisplayName();

					schema.Enum.Add(new OpenApiString(enumMemberValue));

					var fullEnumMemberName = $"F:{fullTypeName}.{enumMemberName}";

					var enumMemberComments = _xmlComments.Descendants("member")
														 .FirstOrDefault(member =>
														 {
															 var attribute = member.Attribute("name");

															 return attribute != null
																	 && string.Equals(attribute.Value, fullEnumMemberName, StringComparison.OrdinalIgnoreCase);
														 });

					if (enumMemberComments == null)
					{
						continue;
					}

					var summary = enumMemberComments.Descendants("summary").FirstOrDefault();

					if (summary == null)
					{
						continue;
					}

					descriptionBuilder.Append($"<li><i>{enumMemberValue}</i> - {summary.Value.Trim()}</li>");
				}

				descriptionBuilder.Append("</ul>");

				schema.Description = descriptionBuilder.ToString();

				
			}
		}
	}
}
