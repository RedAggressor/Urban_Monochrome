using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Repositories;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;
using System.Text.Json.Serialization;


var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpGlobalExceptionFilter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddHttpClient();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICatalogHttpService, CatalogHttpService>();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();

builder.Services
    .AddDbContextFactory<OrderDbContext>(options =>
        options.UseNpgsql(configuration["ConnectionString"]));

builder.Services
    .AddScoped<IDbContextWrapper<OrderDbContext>, DbContextWrapper<OrderDbContext>>();

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseSwagger()
    .UseSwaggerUI(option =>
        option.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Catalog.API V1"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

CreateDbIfNotExists(app);

app.Run();


IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder();

    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
            var context = services.GetRequiredService<OrderDbContext>();

            InitialDbOrder.Initialize(context).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
