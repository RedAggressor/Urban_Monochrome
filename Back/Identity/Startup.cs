using IdentityServer.Data;
using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        { 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionString"]));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddApiEndpoints();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoleService, RoleService>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        { 
            app.UseDeveloperExceptionPage();
                        
            app.UseIdentityServer();

            app.UseCookiePolicy(new CookiePolicyOptions 
            { 
                MinimumSameSitePolicy = SameSiteMode.Strict 
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();            

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            var roleService = serviceProvider.GetRequiredService<IRoleService>();
            roleService.CreateRoleAsync().Wait();
        }
    }
}
