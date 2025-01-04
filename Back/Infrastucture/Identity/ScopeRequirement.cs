using Microsoft.AspNetCore.Authorization;

namespace Infrastucture.Identity
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public ScopeRequirement() 
        { }
    }
}
