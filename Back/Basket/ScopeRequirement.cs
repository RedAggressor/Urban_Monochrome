using Microsoft.AspNetCore.Authorization;

namespace Basket.Host
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public ScopeRequirement() 
        { }
    }
}
