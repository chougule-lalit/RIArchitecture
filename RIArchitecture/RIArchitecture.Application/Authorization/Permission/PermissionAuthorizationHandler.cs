using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RIArchitecture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Authorization.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public PermissionAuthorizationHandler(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            var user = await _userManager.FindByNameAsync(context.User.Identity.Name);
            if (user != null)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);
                var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

                foreach (var role in userRoles)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY")
                                                .Select(x => x.Value);

                    if (permissions.Any())
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"User not found in PermissionAuthorizationHandler");
            }
        }
    }
}
