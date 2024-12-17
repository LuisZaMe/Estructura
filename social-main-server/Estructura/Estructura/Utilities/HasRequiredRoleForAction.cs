using Estructura.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Estructura.API.Utilities
{
    public class HasRequiredRoleForAction: IAuthorizationRequirement
    {
        public List<Role> userRole { get; }
        public HasRequiredRoleForAction(List<Role> userRole)
        {
            this.userRole = userRole;
        }
    }
}
