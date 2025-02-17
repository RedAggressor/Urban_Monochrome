﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastucture.Filters
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType != null &&
                (context.MethodInfo.DeclaringType
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any() ||
                context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any());

            var allowAnonymous = context.MethodInfo.DeclaringType != null &&
                (context.MethodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>()
                    .Any() ||
                context.MethodInfo
                    .GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>()
                    .Any());

            if (!hasAuthorize || allowAnonymous)
            {
                return;

            }

            operation.Responses.TryAdd(
                "401",
                new OpenApiResponse
                { 
                    Description = "Unauthorized"
                });

            operation.Responses.TryAdd(
                "403",
                new OpenApiResponse
                { 
                    Description = "Forbidden"
                });

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [oAuthScheme] = new[] 
                    { 
                        "swagger",
                        "mvc"
                    }
                }
            };
        }
    }
}
