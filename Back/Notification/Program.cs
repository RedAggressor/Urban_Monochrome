using Microsoft.OpenApi.Models;
using Nitifacation.Host.Configs;
using Nitifacation.Host.Services;
using Nitifacation.Host.Services.Interfaces;

var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    option => option.Filters.Add<HttpGlobalExceptionFilter>())
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(
    options => options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Urban Monochrome - Notification HTTP API",
        Version = "v1",
        Description = "The Notification Service HTTP API"
    }));

builder.Services.AddHttpClient();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<ISendPulseService, SendMailService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();

builder.Services.Configure<CredentialConfig>(configuration.GetSection("Credential"));

builder.Services.AddCors(options => 
    options.AddPolicy("AllowALL",
        builder =>
             builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()                   
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowALL");

app.UseSwagger()
    .UseSwaggerUI(option =>
        option.SwaggerEndpoint(
            $"{configuration["PathBase"]}/swagger/v1/swagger.json",
            "Notification.API V1"
        )
    );

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
