using Infrastucture.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ClientConfig>(
                builder.Configuration.GetSection("Client"));
            builder.Services.Configure<AuthorizationConfig>(
                builder.Configuration.GetSection("Authorization"));
        }
    }
}
