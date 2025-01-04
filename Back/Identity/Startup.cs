using System.IO;
using IdentityServer.Data;
using IdentityServer.Quickstart;
using Infrastucture.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();

            services.AddControllers(option => option.Filters.Add<HttpGlobalExceptionFilter>())
                .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

            //services.Configure<AppSettings>(configuration);

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionString"]));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddApiEndpoints();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients(configuration))
                .AddTestUsers(TestUsers.Users)
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential();

            //services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage(); 

            app.UseIdentityServer();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}