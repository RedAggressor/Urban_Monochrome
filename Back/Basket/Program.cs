using Basket.Host.Configs;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Basket.Host;


var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(option => option.Filters.Add<HttpGlobalExceptionFilter>())
    .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Urban Monochrome - Basket HTTP API",
        Version = "v1",
        Description = "The Basket Service Http Api"
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("http://localhost:5001/connect/token")
            }
        }
    });

    var oAuthScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
    };

    options.AddSecurityRequirement(
       new OpenApiSecurityRequirement
       {
           [oAuthScheme] = new[] { "swagger" }
       });
});

builder.Services.Configure<RedisConfig>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

builder.Services
    .AddAuthentication()
    .AddJwtBearer("Internal", options =>
    {
        options.Authority = "http://localhost:5001";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    })
    .AddJwtBearer("Site", options =>
    {
        options.Authority = "http://localhost:5001";
        options.Audience = "localhost";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowEndUser", policy =>
    {
        policy.AuthenticationSchemes.Add("Site");
        policy.RequireClaim(JwtRegisteredClaimNames.Sub);
    });
    options.AddPolicy("AllowClient", policy =>
    {
        policy.AuthenticationSchemes.Add("Internal");
        policy.Requirements.Add(new DenyAnonymousAuthorizationRequirement());
        policy.Requirements.Add(new ScopeRequirement());
    });
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.Configure<ClientConfig>(
            builder.Configuration.GetSection("Client"));

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options => 
//    { 
//        options.Authority = "http://localhost:5001";
//        options.RequireHttpsMetadata = false;
//        options.Audience = "api1";
//    });

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IRedisCacheConnectionService, RedisCacheConnectionService>();
builder.Services.AddTransient<IJsonSerializerService, JsonSerializerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.SetIsOriginAllowed((host) => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseSwagger()
    .UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Basket.API V1");
        setup.OAuthClientId("basketswaggerui");
        //setup.OAuthClientSecret("secret");
        setup.OAuthAppName("Basket Swagger UI");
        //setup.OAuthUsePkce();
        
    });

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

    return builder.Build();
}
