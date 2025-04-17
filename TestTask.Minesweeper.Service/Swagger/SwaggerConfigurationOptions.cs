using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestTask.Minesweeper.Service.Swagger
{
	/// <summary>
	/// Represents options for swagger configuration.
	/// </summary>
	internal sealed class SwaggerConfigurationOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private static readonly string _title = typeof(Program).Assembly.GetName().Name ?? "Title";

		private readonly IApiVersionDescriptionProvider _provider;

		/// <summary>
		/// Initializes a new instance of <see cref="SwaggerConfigurationOptions"/>.
		/// </summary>
		/// <param name="provider">Instance of <see cref="IApiVersionDescriptionProvider"/> - api version description provider.</param>
		/// <exception cref="ArgumentNullException"><paramref name="provider"/> cannot be <see langword="null"/>.</exception>
		public SwaggerConfigurationOptions(IApiVersionDescriptionProvider provider)
		{
	 		_provider = provider ?? throw new ArgumentNullException(nameof(provider));
		}

		/// <inheritdoc/>
		public void Configure(SwaggerGenOptions options)
		{
			foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
			}
		}

		/// <inheritdoc/>
		public void Configure(string? name, SwaggerGenOptions options)
		{
			Configure(options);
		}

		/// <summary>
		/// Creates information about api version.
		/// </summary>
		/// <param name="apiVersionDescription">Instance of <see cref="ApiVersionDescription"/> - api version description.</param>
		/// <returns>Instance of <see cref="OpenApiInfo"/> - Open API metadata.</returns>
		private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
		{
			var info = new OpenApiInfo()
			{
				Title = _title,
				Version = apiVersionDescription.ApiVersion.ToString()
			};

			if (apiVersionDescription.IsDeprecated)
			{
				info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
			}

			return info;
		}
	}
}
