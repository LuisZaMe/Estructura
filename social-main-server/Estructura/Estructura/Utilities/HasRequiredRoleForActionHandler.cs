using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Estructura.API.Utilities
{
    public class HasRequiredRoleForActionHandler:AuthorizationHandler<HasRequiredRoleForAction>, IAuthorizationHandler
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  HasRequiredRoleForAction requirement)
        {

            if(context.User.HasClaim(e => e.Type.Equals(Core.Models.AppUser.CLAIM_USER_ROLE)))
            {
                var currentRole = context.User.Claims.FirstOrDefault(c=>c.Type ==Core.Models.AppUser.CLAIM_USER_ROLE);
                if (requirement.userRole.Any(r=> r == (Common.Enums.Role)int.Parse(currentRole.Value)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
