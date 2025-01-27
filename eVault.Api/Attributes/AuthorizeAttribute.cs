using eVault.Domain.Enums;
using eVault.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace eVault.Api.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string?[] _requiredRoles;

        public AuthorizeRoleAttribute()
        {
            _requiredRoles = null;
        }

        public AuthorizeRoleAttribute(params eUserRole[] requiredRoles)
        {
            _requiredRoles = requiredRoles.Select(role => role.GetDescription()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(_requiredRoles != null && _requiredRoles.Any())
            {
                var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

                if (!_requiredRoles.Any(role => userRoles.Contains(role)))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }
}