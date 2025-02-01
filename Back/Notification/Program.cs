using Infrastucture.Extensions;
using Microsoft.OpenApi.Models;
using Nitifacation.Host.Configs;
using Nitifacation.Host.Services;
using Nitifacation.Host.Services.Interfaces;

var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    option => option.Filters.Add<HttpGlobalExceptionFilter>())
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Urban Monochrome - Order HTTP API",
        Version = "v1",
        Description = "The Order Service HTTP API"
    });

    var authority = configuration["Authorization:Authority"];
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                TokenUrl = new Uri($"{authority}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"mvc", "MVC Application" }
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

builder.AddConfiguration();

builder.Services.AddAuthorization(configuration);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<ISendPulseService, SendMailService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();

builder.Services.Configure<CredentialConfig>(configuration.GetSection("Credential"));

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials())
    );

var app = builder.Build();


app.UseCors("CorsPolicy");

app.UseSwagger()
    .UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Notification.API V1");
        option.OAuthClientId("notificationswaggerui");
        option.OAuthAppName("Notification Swagger UI");
    });

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder();

    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}
