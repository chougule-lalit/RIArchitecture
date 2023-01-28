using Microsoft.AspNetCore.Identity;
using RIArchitecture.Application.Contracts.Utility.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Authorization
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsDto> allPermissions, Type permissionType, string roleId)
        {
            //List<FieldInfo> fields = new List<FieldInfo>();
            //foreach (var policy in policies)
            //{
            //    fields.AddRange(policy.GetFields(BindingFlags.Static | BindingFlags.Public));
            //}

            //foreach (FieldInfo fi in fields)
            //{
            //    allPermissions.Add(new RoleClaimsDto { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            //}


            int level = 0;
            foreach (var type in permissionType.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!type.IsAbstract)
                {
                    continue;
                }

                var parentName = type.Name;
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    allPermissions.Add(new RoleClaimsDto
                    {
                        Level = level,
                        Value = field.GetValue(null).ToString(),
                        Type = "Permissions",
                        DisplayName = field.Name,
                        ParentName = parentName
                    });
                }
                level++;
            }
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole<Guid>> roleManager, IdentityRole<Guid> role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
