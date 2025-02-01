using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder host = null;

            try 
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog((ctx, lc) => lc
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(ctx.Configuration));

                host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
                Log.Fatal(ex, "Unhandled exception");                
            }
            finally
            {
                Log.Information("Shut down complete");
                Log.CloseAndFlush();
                //host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);                
            }

            return host;
        }           
    }
}