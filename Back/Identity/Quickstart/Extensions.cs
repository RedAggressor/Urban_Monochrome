using Duende.IdentityServer.Stores;
using System.Threading.Tasks;

namespace IdentityServer.Quickstart
{
    public static class Extensions
    {        
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }
    }
}
