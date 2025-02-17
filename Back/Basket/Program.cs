using Basket.Host.Configs;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Infrastucture.Extensions;
using Microsoft.OpenApi.Models;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(option => 
    option.Filters.Add<HttpGlobalExceptionFilter>())
    .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Urban Monochrome - Basket HTTP API",
        Version = "v1",
        Description = "The Basket Service HTTP API"
    });

    var authority = configuration["Authorization:Authority"];
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                TokenUrl = new Uri($"{authority}/connect/token"),
                Scopes = new Dictionary<string, string>()
                {
                    { "mvc", "MVC Application" }                   
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

builder.AddConfiguration();

builder.Services.Configure<RedisConfig>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddAuthorization(configuration);

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IRedisCacheConnectionService, RedisCacheConnectionService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();
builder.Services.AddTransient<ILikeItemService, LikeItemService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseSwagger()
    .UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Basket.API V1");
        setup.OAuthClientId("basketswaggerui");
        setup.OAuthAppName("Basket Swagger UI");
    });

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) => {
    Console.WriteLine($"/Claims:{context?.User?.Claims} ");
    foreach (var claim in context?.User?.Claims!)
    {
        Console.WriteLine(claim);
        Console.WriteLine();
    }
    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

