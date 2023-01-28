using Microsoft.AspNetCore.Identity;
using RIArchitecture.Core;
using RIArchitecture.Core.Constants;
using System;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Administration.Seeding
{
    public static class RIArchitectureRoleSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(RoleType.SuperAdmin.ToString()))
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.SuperAdmin.ToString()));

            if (!await roleManager.RoleExistsAsync(RoleType.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Admin.ToString()));

            if (!await roleManager.RoleExistsAsync(RoleType.Basic.ToString()))
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Basic.ToString()));
        }
    }
}
