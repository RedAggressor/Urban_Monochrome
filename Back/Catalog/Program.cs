using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services;
using Catalog.Host.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpGlobalExceptionFilter>();
})
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Urban Monochrome - Catalog HTTP API",
        Version = "v1",
        Description = "The Catalog Service HTTP API"
    });

    var authority = configuration["Authorization:Authority"];
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows 
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

builder.Services.Configure<CatalogConfig>(configuration);

builder.Services.AddAuthorization(configuration);

builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IColorRepository, ColorRepository>();
builder.Services.AddTransient<IColorService, ColorService>();
builder.Services.AddTransient<ISizeRepository, SizeRepository>();
builder.Services.AddTransient<ISizeService, SizeService>();
builder.Services.AddTransient<IItemSpecificationRepository, ItemSpecificationRepository>();
builder.Services.AddTransient<IItemSpecificationService, ItemSpecificationService>();
builder.Services.AddTransient<ITypeRepository, TypeRepository>();
builder.Services.AddTransient<ITypeService, TypeService>();
builder.Services.AddTransient<IGroupeRepository, GroupeRepository>();
builder.Services.AddTransient<IGroupeService, GroupeService>();

builder.Services
    .AddDbContextFactory<CatalogDbContext>(options => 
        options.UseNpgsql(configuration["ConnectionString"]));

builder.Services
    .AddScoped<IDbContextWrapper<CatalogDbContext>, DbContextWrapper<CatalogDbContext>>();

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials())
    );

var app = builder.Build();

app.UseSwagger()
    .UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Catalog.API V1");
        option.OAuthClientId("catalogswaggerui");
        option.OAuthAppName("Catalog Swagger UI");
    });

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

CreateDbIfNotExists(app);

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder();

    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional:false, reloadOnChange: true)
        .AddEnvironmentVariables();   

    return builder.Build();
}

void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CatalogDbContext>();

            InitializeDatabase.Initialize(context).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
