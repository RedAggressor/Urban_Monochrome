using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Duende.IdentityServer.Models;

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
                new ApiResource("www.liqpay.ua")
                {
                    Scopes = new List<string>
                    {
                        "react"
                    }
                },
                new ApiResource("localhost")
                {
                    Scopes = new List<string>
                    {
                        "mvc"
                    },
                },
                new ApiResource("catalog")
                {
                    Scopes = new List<string>
                    {
                        "catalog.catalogbff",
                        "catalog.catalogitem"
                    },
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope> 
            { 
                new ApiScope("mvc", "MVC Application"),
                new ApiScope("react", "React Application"),
                new ApiScope("catalog.catalogbff", "Catalog BFF"),
                new ApiScope("catalog.catalogitem", "Catalog Item")
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
                        new Secret("secret".Sha256())
                    },
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
                        "catalog.catalogbff", "catalog.catalogitem", "react", "mvc"
                    }
                },
                new Client
                {
                    ClientId = "basket",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc"
                    }
                }
            };
        }
    }
}