using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1", "My test Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
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
                    //RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    //ClientSecrets = { new Secret("your-client-secret".Sha256()) },
                    RedirectUris = { "http://localhost:5003/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/swagger/" },
                    AllowedScopes = { "api1", "openid", "profile"}
                }   
            };
        }
    }
}
