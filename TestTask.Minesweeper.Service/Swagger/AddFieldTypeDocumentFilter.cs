using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestTask.Minesweeper.Service.Swagger
{
	/// <summary>
	/// Represents a <see cref="IDocumentFilter"/> to add <see cref="Api.FieldType"/> to swagger documentation.
	/// </summary>
	internal sealed class AddFieldTypeDocumentFilter : IDocumentFilter
	{
		/// <inheritdoc/>
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			context.SchemaGenerator.GenerateSchema(typeof(Api.FieldType), context.SchemaRepository);
		}
	}
}
