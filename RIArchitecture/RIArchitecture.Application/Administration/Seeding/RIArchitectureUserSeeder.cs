using Microsoft.AspNetCore.Identity;
using RIArchitecture.Application.Authorization;
using RIArchitecture.Core;
using RIArchitecture.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Administration.Seeding
{
    public static class RIArchitectureUserSeeder
    {
        public static async Task SeedBasicUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var defaultUser = new AppUser
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Password@123");
                    await userManager.AddToRoleAsync(defaultUser, RoleType.Basic.ToString());
                }
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var defaultUser = new AppUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Password@123");
                    await userManager.AddToRoleAsync(defaultUser, RoleType.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, RoleType.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, RoleType.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Items");
            await roleManager.AddPermissionClaim(adminRole, "Users");
            await roleManager.AddPermissionClaim(adminRole, "Roles");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole<Guid>> roleManager, IdentityRole<Guid> role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);

            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
