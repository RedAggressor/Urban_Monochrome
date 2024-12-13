using Basket.Host.Configs;
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

builder.Services.Configure<RedisConfig>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IRedisCacheConnectionService, RedisCacheConnectionService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();
builder.Services.AddTransient<ILikeItemService, LikeItemService>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("AllowAll");

app.UseSwagger().UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint($"{configuration["BasePath"]}/swagger/v1/swagger.json",
        "Basket.API V1");    
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
