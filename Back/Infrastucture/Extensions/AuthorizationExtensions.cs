using Infrastucture.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using IdentityModel;


namespace Infrastucture.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration["Authorization:Authority"];
            var siteAudience = configuration["Authorization:SiteAudience"];

            services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

            services.AddAuthentication()
                .AddJwtBearer(AuthScheme.Internal, options =>
                {
                    options.Authority = authority;                    
                    options.TokenValidationParameters.ValidateAudience = false; 
                    options.RequireHttpsMetadata = false;
                    //options.MapInboundClaims = false;
                })
                .AddJwtBearer(AuthScheme.Site, options =>
                {
                    options.Authority = authority;
                    options.Audience = siteAudience;                    
                    options.RequireHttpsMetadata = false;
                    options.MapInboundClaims = false;
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicy.AllowEndUserPolicy, policy =>
                {
                    policy.AuthenticationSchemes.Add(AuthScheme.Site);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Subject);
                });

                options.AddPolicy(AuthPolicy.AllowClientPolicy, policy =>
                {
                    policy.AuthenticationSchemes.Add(AuthScheme.Internal);
                    policy.Requirements.Add(new DenyAnonymousAuthorizationRequirement());                    
                    policy.Requirements.Add(new ScopeRequirement());
                });

                options.AddPolicy(AuthPolicy.AllowRoleUserPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(AuthScheme.Site);
                    policy.RequireAuthenticatedUser();                    
                    policy.RequireRole("Moderator", "Admin");
                });
            });
        }
    }
}
