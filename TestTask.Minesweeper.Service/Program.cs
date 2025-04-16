using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Serilog;
using Serilog.Events;

using TestTask.Minesweeper.Application.Bootstrap;
using TestTask.Minesweeper.Persistence;
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
		public static async Task<int> Main(string[] args)
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

				var application = await InitializeApplicationAsync(builder);

				await application.RunAsync(CancellationToken.None);

				Log.Information("Execution of service was ended.");

				return 0;
			}
			catch (HostAbortedException)
			{
				Log.Information("Execution of host was aborted.");

				throw;
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

			ConfigureServices(builder.Services, builder.Environment, builder.Configuration);

			return builder;
		}

		private static void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
		{
			var jsonOptions = SetupJsonOptions(environment.IsDevelopment());

			services.AddSingleton<JsonSerializerOptions>(jsonOptions)
					.AddControllers()
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

			services
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

			services
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

			services.ConfigureOptions<SwaggerConfigurationOptions>();

			services.AddLogging()
					.AddSerilog(configureLogger =>
					{
						configureLogger.ReadFrom.Configuration(configuration);
					});

			services.AddProblemDetails(options =>
			{
				options.IncludeExceptionDetails = (context, exception) => environment.IsDevelopment();

				options.Ignore<Application.Exceptions.ValidationFaultException>();

				options.Ignore<Application.Exceptions.ApplicationFaultBusinessException>();

				options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

				options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
			});

			services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(Application.Bootstrap.ServiceCollectionExtensions).Assembly));

			AddFluentValidation(services, typeof(Application.Bootstrap.ServiceCollectionExtensions).Assembly);

			services.AddAutoMapper(typeof(Program).Assembly, typeof(Application.Bootstrap.ServiceCollectionExtensions).Assembly);

			services.AddMinesweeperApplication(configuration);
		}

		private static void BuildConfiguration(WebApplicationBuilder builder, string[] arguments)
		{
			builder.Configuration
					.AddJsonFile("appsettings.json")
					.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
					.AddEnvironmentVariables()
					.AddCommandLine(arguments);
		}

		/// <summary>
		/// Adds validators to <paramref name="services"/>, which are contained in specified <paramref name="assemblies"/>.
		/// </summary>
		/// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
		/// <param name="assemblies">Assemblies.</param>
		/// <returns>Instance of <see cref="IServiceCollection" />.</returns>
		private static IServiceCollection AddFluentValidation(IServiceCollection services, params Assembly[] assemblies)
		{
			ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

			AssemblyScanner.FindValidatorsInAssemblies(assemblies)
				.ForEach(pair =>
				{
					services.TryAddEnumerable(ServiceDescriptor.Scoped(pair.InterfaceType, pair.ValidatorType));
				});

			return services;
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

		private static async Task<WebApplication> InitializeApplicationAsync(WebApplicationBuilder builder)
		{
			WebApplication application = builder.Build();

			await application.Services.ApplyDatabaseMigrationsAsync<Persistence.GameDbContext>();

			application.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

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

			application.UseMiddleware<Middlewares.ProcessableExceptionsMiddleware>();

			application.UseProblemDetails();

			application.UseMiddleware<Middlewares.CancellationSuppressionMiddleware>();

			application.UseRouting();

			application.MapControllers();

			return application;
		}
	}
}
