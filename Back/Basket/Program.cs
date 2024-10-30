using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Microsoft.OpenApi.Models;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(option => 
    option.Filters.Add<HttpGlobalExceptionFilter>())
    .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Urban Monochrome - Basket HTTP API",
        Version = "v1",
        Description = "The Basket Service Http Api"
    });
});

//builder.Services.Configure<RedisConfig>(
    //builder.Configuration.GetSection("Redis"));

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IRedisCacheConnectionService, RedisCacheConnectionService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger().UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint($"{configuration["BasePath"]}/swagger/v1/swagger.json", "Basket.API V1");
    setup.OAuthClientId("basketswaggerui");
    setup.OAuthAppName("Basket Swagger UI");
});

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}
