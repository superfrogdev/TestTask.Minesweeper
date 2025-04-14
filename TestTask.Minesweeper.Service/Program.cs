using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Hellang.Middleware.ProblemDetails;

using Serilog;
using Serilog.Events;

using TestTask.Minesweeper.Service.Swagger;

namespace TestTask.Minesweeper.Service
{
	/// <summary>
	/// Represents a main class of this service.
	/// </summary>
	public sealed class Program
	{
		/// <summary>
		/// Does start of service execution.
		/// </summary>
		/// <param name="args">Command-line arguments.</param>
		public static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
								.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
								.Enrich.FromLogContext()
								.WriteTo.Console()
								.CreateBootstrapLogger();

			Log.Information("Execution of service was started.");

			try
			{

				WebApplicationBuilder builder = CreateBuilder(args);

				var application = InitializeApplication(builder);

				application.Run();

				Log.Information("Execution of service was ended.");

				return 0;
			}
			catch (Exception exception)
			{
				Log.Fatal(exception, "Execution of service was terminated with unprocessed error.");

				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		/// <summary>
		/// Creates a <see cref="WebApplicationBuilder"/>.
		/// </summary>
		/// <param name="arguments">Command-line arguments.</param>
		/// <returns>Instance of <see cref="WebApplicationBuilder"/>.</returns>
		private static WebApplicationBuilder CreateBuilder(string[] arguments)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(arguments);

			BuildConfiguration(builder, arguments);

			var jsonOptions = SetupJsonOptions(builder.Environment.IsDevelopment());

			builder.Services.AddSingleton<JsonSerializerOptions>(jsonOptions);
			
			builder.Services.AddControllers()
				.AddJsonOptions(configure =>
				{
					var destinationJsonOptions = configure.JsonSerializerOptions;

					destinationJsonOptions.AllowTrailingCommas = jsonOptions.AllowTrailingCommas;
					destinationJsonOptions.DefaultBufferSize = jsonOptions.DefaultBufferSize;
					destinationJsonOptions.DefaultIgnoreCondition = jsonOptions.DefaultIgnoreCondition;
					destinationJsonOptions.DictionaryKeyPolicy = jsonOptions.DictionaryKeyPolicy;
					destinationJsonOptions.Encoder = jsonOptions.Encoder;
					destinationJsonOptions.IgnoreReadOnlyFields = jsonOptions.IgnoreReadOnlyFields;
					destinationJsonOptions.IgnoreReadOnlyProperties = jsonOptions.IgnoreReadOnlyProperties;
					destinationJsonOptions.IncludeFields = jsonOptions.IncludeFields;
					destinationJsonOptions.MaxDepth = jsonOptions.MaxDepth;
					destinationJsonOptions.NumberHandling = jsonOptions.NumberHandling;
					destinationJsonOptions.PreferredObjectCreationHandling = jsonOptions.PreferredObjectCreationHandling;
					destinationJsonOptions.PropertyNameCaseInsensitive = jsonOptions.PropertyNameCaseInsensitive;
					destinationJsonOptions.PropertyNamingPolicy = jsonOptions.PropertyNamingPolicy;
					destinationJsonOptions.ReadCommentHandling = jsonOptions.ReadCommentHandling;
					destinationJsonOptions.ReferenceHandler = jsonOptions.ReferenceHandler;					

					destinationJsonOptions.UnknownTypeHandling = jsonOptions.UnknownTypeHandling;
					destinationJsonOptions.UnmappedMemberHandling = jsonOptions.UnmappedMemberHandling;
					destinationJsonOptions.WriteIndented = jsonOptions.WriteIndented;

					foreach (var current in jsonOptions.Converters)
					{
						destinationJsonOptions.Converters.Add(current);
					}
				});

			builder.Services
				.AddApiVersioning(setupAction =>
				{
					setupAction.DefaultApiVersion = new ApiVersion(1, 0);
					setupAction.AssumeDefaultVersionWhenUnspecified = true;
					setupAction.ReportApiVersions = true;
					setupAction.ApiVersionReader = new UrlSegmentApiVersionReader();
				})
				.AddApiExplorer(setupAction =>
				{
					setupAction.GroupNameFormat = "'v'VVV";
					setupAction.SubstituteApiVersionInUrl = true;
				});

			builder.Services
				.AddSwaggerGen(setupAction =>
				{
					var fileNames = new string[] { typeof(Program).Assembly.GetName().Name + ".xml" };

					foreach (var currentFileName in fileNames)
					{
						var xmlPath = Path.Combine(AppContext.BaseDirectory, currentFileName);

						setupAction.IncludeXmlComments(xmlPath, true);

						setupAction.SchemaFilter<EnumTypesSchemaFilter>(xmlPath);

						setupAction.DocumentFilter<AddFieldTypeDocumentFilter>();
					}

					setupAction.CustomSchemaIds(type => type.FullName);

					setupAction.MapType<DateTimeOffset>(() => new Microsoft.OpenApi.Models.OpenApiSchema()
					{
						Type = "string",
						Format = "date-time",
						Example = new Microsoft.OpenApi.Any.OpenApiDateTime(DateTimeOffset.UtcNow)
					});

					setupAction.MapType<Api.FieldType[,]>(() =>
					{
						var array = new Microsoft.OpenApi.Any.OpenApiArray();

						var firstSubArray = new Microsoft.OpenApi.Any.OpenApiArray
						{
							new Microsoft.OpenApi.Any.OpenApiString(Api.FieldType.One.GetDisplayName()),
							new Microsoft.OpenApi.Any.OpenApiString(Api.FieldType.Mine.GetDisplayName())
						};

						array.Add(firstSubArray);

						var secondSubArray = new Microsoft.OpenApi.Any.OpenApiArray
						{
							new Microsoft.OpenApi.Any.OpenApiString(Api.FieldType.One.GetDisplayName()),
							new Microsoft.OpenApi.Any.OpenApiString(Api.FieldType.One.GetDisplayName())
						};

						array.Add(secondSubArray);

						var schema = new Microsoft.OpenApi.Models.OpenApiSchema()
						{
							Type = "array",
							Items = new Microsoft.OpenApi.Models.OpenApiSchema()
							{
								Type = "array",
								Items = new Microsoft.OpenApi.Models.OpenApiSchema()
								{
									Type = "enum",
									Reference = new Microsoft.OpenApi.Models.OpenApiReference() { Id = "TestTask.Minesweeper.Service.Api.FieldType", Type = Microsoft.OpenApi.Models.ReferenceType.Schema }
								}
							},
							Example = array
						};

						return schema;
					});
				})
				.AddHttpLogging(configureOptions =>
				{
					configureOptions.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All
														| Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery;
				});

			builder.Services.ConfigureOptions<SwaggerConfigurationOptions>();

			builder.Services.AddLogging()
							.AddSerilog(configureLogger =>
							{
								configureLogger.ReadFrom.Configuration(builder.Configuration);
							});

			builder.Services.AddProblemDetails(options =>
			{
				options.IncludeExceptionDetails = (context, exception) => builder.Environment.IsDevelopment();

				options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

				options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
			});

			return builder;
		}

		private static void BuildConfiguration(WebApplicationBuilder builder, string[] arguments)
		{
			builder.Configuration
					.AddJsonFile("appsettings.json")
					.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
					.AddEnvironmentVariables()
					.AddCommandLine(arguments);
		}

		private static JsonSerializerOptions SetupJsonOptions(bool isDevelopmentEnvironment)
		{
			var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};

			options.WriteIndented = isDevelopmentEnvironment;

			options.Converters.Add(new JsonStringEnumConverter<Api.FieldType>());
			options.Converters.Add(new Api.Json.FieldJsonConverter());

			options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin);

			return options;
		}

		private static WebApplication InitializeApplication(WebApplicationBuilder builder)
		{
			WebApplication application = builder.Build();

			application.UseHttpLogging();

			if (application.Environment.IsDevelopment())
			{
				application.UseSwagger();
				application.UseSwaggerUI(setupAction =>
				{
					var descriptionProvider = application.Services.GetRequiredService<IApiVersionDescriptionProvider>();

					foreach (ApiVersionDescription description in descriptionProvider.ApiVersionDescriptions)
					{
						setupAction.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
					}
				});
			}
			else
			{
				application.UseHsts();
			}

			application.UseHttpsRedirection();

			application.UseProblemDetails();

			application.UseRouting();

			application.MapControllers();

			return application;
		}
	}
}
