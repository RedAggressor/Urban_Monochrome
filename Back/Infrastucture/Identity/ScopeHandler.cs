﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Infrastucture.Identity
{
    public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            var targetScope = GetTargetScope(context);
            if (targetScope != null)
            {
                var scopes = context.User.Claims.Where(c => c.Type == "scope")
                    .SelectMany(x => x.Value.Split(' ')).ToArray();
                if (scopes.Contains(targetScope)) 
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private string? GetTargetScope(AuthorizationHandlerContext context)
        {
            var httpContext = (HttpContext)context.Resource!;

            var routeEndpoint = httpContext.GetEndpoint();
            var descriptor = routeEndpoint?.Metadata
                .OfType<ControllerActionDescriptor>()
                .SingleOrDefault();

            if (descriptor != null)
            {
                var scopeAttribute = (ScopeAttribute?)descriptor.MethodInfo.GetCustomAttribute(typeof(ScopeAttribute))
                                     ?? (ScopeAttribute?)descriptor.ControllerTypeInfo.GetCustomAttribute(typeof(ScopeAttribute));

                return scopeAttribute?.ScopeName;
            }

            return null;
        }
    }
}
