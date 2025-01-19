using System.IO;
using Duende.IdentityServer;
using IdentityServer.Data;
using IdentityServer.Quickstart;
using Infrastucture.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            //services.AddControllers(option => option.Filters.Add<HttpGlobalExceptionFilter>())
            //    .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);           

            services.Configure<AppSettings>(configuration);

            //services.AddDbContext<UserDbContext>(options =>
            //    options.UseNpgsql(configuration["ConnectionString"]));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<UserDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
                //options.EmitScopesAsSpaceDelimitedStringInJwt = true;
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients(configuration))
                .AddTestUsers(TestUsers.Users);
                //.AddAspNetIdentity<IdentityUser>()
                //.AddDeveloperSigningCredential();

            services.AddAuthentication()
           .AddGoogle(options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                              
               options.ClientId = "copy client ID from Google here";
               options.ClientSecret = "copy client secret from Google here";
           });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage(); 

            app.UseIdentityServer();
            app.UseCookiePolicy(new CookiePolicyOptions 
            { 
                MinimumSameSitePolicy = SameSiteMode.Strict,
                Secure = CookieSecurePolicy.None
            });
                        
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}