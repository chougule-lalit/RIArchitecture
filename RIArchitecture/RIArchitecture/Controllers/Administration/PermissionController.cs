using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIArchitecture.Application.Authorization;
using RIArchitecture.Application.Contracts.Utility.Permission;
using RIArchitecture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.Administration
{
    public class PermissionController : RIArchitectureBaseApiController
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public PermissionController(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetPermissionForRole/{roleId}")]
        [Authorize(Policy = Permissions.Roles.PermissionChange)]
        public async Task<IActionResult> GetPermissionForRoleAsync(string roleId)
        {
            PermissionDto model = await GetPermissionsByRoleId(roleId);
            return Ok(model);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Policy = Permissions.Roles.PermissionChange)]
        public async Task<IActionResult> Update(PermissionDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.IsGranted).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            return Ok(true);
        }

        [HttpGet]
        [Route("GetAllPermissionForUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllPermissionForUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return Ok("User not found!!!");

            var roles = await _userManager.GetRolesAsync(user);
            var roleIds = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).Select(x => x.Id).ToListAsync();
            var allPermissions = new List<RoleClaimsDto>();
            foreach (var roleId in roleIds)
            {
                var permissionData = await GetPermissionsByRoleId(roleId.ToString());
                if(permissionData != null)
                {
                    allPermissions.AddRange(permissionData.RoleClaims);
                    allPermissions.RemoveAll(x => !x.IsGranted);
                }
            }
            return Ok(allPermissions.Select(x=> x.Value).ToList());
        }

        private async Task<PermissionDto> GetPermissionsByRoleId(string roleId)
        {
            var model = new PermissionDto();
            var allPermissions = new List<RoleClaimsDto>();
            allPermissions.GetPermissions(typeof(Permissions), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.IsGranted = true;
                }
            }
            model.RoleClaims = allPermissions;
            return model;
        }
    }
}
