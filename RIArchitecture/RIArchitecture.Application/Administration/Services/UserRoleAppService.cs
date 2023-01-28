using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RIArchitecture.Application.Administration.Seeding;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility.Permission;
using RIArchitecture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Administration.Services
{
    public class UserRoleAppService : IUserRoleAppService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public UserRoleAppService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async virtual Task<ManageUserRolesDto> GetUserRoleAsync(string userId)
        {
            var userRoles = new List<UserRolesDto>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var roleName in roles.Select(x => x.Name))
            {
                var userRolesViewModel = new UserRolesDto
                {
                    RoleName = roleName
                };
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                userRoles.Add(userRolesViewModel);
            }
            var userRolesDetails = new ManageUserRolesDto()
            {
                UserId = userId,
                UserRoles = userRoles
            };

            return userRolesDetails;
        }

        public async virtual Task<bool> Update(ManageUserRolesDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
                return false;
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));

            if (!result.Succeeded)
                return false;

            //await _signInManager.RefreshSignInAsync(user);
            await RIArchitectureUserSeeder.SeedSuperAdminAsync(_userManager, _roleManager);
            return true;
        }
    }
}
