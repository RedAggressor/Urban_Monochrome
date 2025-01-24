using System.IO;
using Duende.IdentityServer;
using IdentityServer.Data;
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

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddControllers(option => option.Filters.Add<HttpGlobalExceptionFilter>())
                .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

            services.Configure<AppSettings>(configuration);

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionString"]);                
            });
                

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();            

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;                
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients(configuration))               
                .AddAspNetIdentity<IdentityUser>();                          

            var googleClientId = configuration["Authentication:Google:ClientId"];
            var googleClientSecret = configuration["Authentication:Google:ClientSecret"];

            services.AddAuthentication()
           .AddGoogle(options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                              
               options.ClientId = googleClientId;
               options.ClientSecret = googleClientSecret;
           });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                Secure = CookieSecurePolicy.None
            });

            app.UseStaticFiles();
            app.UseRouting();            

            app.UseAuthentication();
            app.UseIdentityServer();            
            app.UseAuthorization();

            SeedUser.SeedUserAsync(app);

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}