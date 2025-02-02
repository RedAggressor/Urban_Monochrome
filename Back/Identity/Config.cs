using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {                
                new ApiResource("urbanmonochrome.com", "Urban Monochrome")
                {
                    Scopes = new List<string>
                    {
                        "mvc"
                    },
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("mvc", "MVC Application"),
                new ApiScope("react", "React Application"),
                new ApiScope("catalog.catalogbff", "Catalog BFF"),
                new ApiScope("catalog.catalogbfs", "Catalog BFS")
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "react_spa",
                    ClientName = "React SPA",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { $"{configuration["ReactClientUrl"]}/callback" },
                    PostLogoutRedirectUris = { $"{configuration["ReactClientUrl"]}/" },
                    AllowedCorsOrigins = { configuration["ReactClientUrl"], "https://www.liqpay.ua" },
                    AllowedScopes = { "openid", "profile", "react", "mvc", "catalog.catalogbff", "catalog.catalogitem"},
                    AllowAccessTokensViaBrowser = true

                },
                new Client
                {
                    ClientId = "catalog",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedScopes =
                    {
                         "mvc", "catalog.catalogbff", "catalog.catalogbfs",// "react",
                    }
                },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc", "catalog.catalogbfs"
                    }
                },
                new Client
                {
                    ClientId = "basket",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,                    
                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedScopes = 
                    {                         
                        "mvc"                        
                    },
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    ClientSecrets = 
                    { 
                        new Secret(configuration["Secret"].Sha256()) 
                    },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {                         
                        "mvc"
                    },
                },
                new Client
                {
                    ClientId = "order",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedScopes =
                    {
                        "mvc", "catalog.catalogbfs"
                    },
                },
                new Client
                {
                    ClientId = "orderswaggerui",
                    ClientName = "Order Swagger UI",
                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["OrderApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["OrderApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc", "catalog.catalogbfs"
                    },
                },
                new Client
                {
                    ClientId = "notification",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedScopes =
                    {
                        "mvc"
                    },
                },
                new Client
                {
                    ClientId = "notificationswaggerui",
                    ClientName = "Notification Swagger UI",
                    ClientSecrets =
                    {
                        new Secret(configuration["Secret"].Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["NotificationApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["NotificationApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc"
                    },
                },
            };
        }
    }
}