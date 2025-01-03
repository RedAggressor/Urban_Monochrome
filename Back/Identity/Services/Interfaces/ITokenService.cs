using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
