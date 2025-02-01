using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ProfileService
            (UserManager<IdentityUser> userManager )
            
        {
            _userManager = userManager;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {            
            var claims = context.Subject.Claims;            

            context.IssuedClaims.AddRange(claims);

            await Task.CompletedTask;
        }
    }
}
