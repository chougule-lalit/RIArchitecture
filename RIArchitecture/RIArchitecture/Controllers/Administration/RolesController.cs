using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIArchitecture.Application.Authorization;
using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.Administration
{
    //[Authorize(Roles = "SuperAdmin")]
    public class RolesController : RIArchitectureBaseApiController, IRoleAppService
    {
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = Permissions.Roles.Create)]
        public virtual async Task<bool> CreateAsync(string roleName)
        {
            return await _roleAppService.CreateAsync(roleName);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Policy = Permissions.Roles.Delete)]
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await _roleAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Policy = Permissions.Roles.View)]
        public virtual async Task<RoleDto> GetAsync(Guid id)
        {
            return await _roleAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("pagedResult")]
        [Authorize(Policy = Permissions.Roles.View)]
        public virtual async Task<PagedResultDto<RoleDto>> PagedResultAsync(GetRoleInputDto input)
        {
            return await _roleAppService.PagedResultAsync(input);
        }

        [HttpPost]
        [Route("roleExist")]
        [Authorize(Policy = Permissions.Roles.View)]
        public Task<bool> RoleExistAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Policy = Permissions.Roles.Edit)]
        public virtual async Task<bool> UpdateAsync(RoleDto input)
        {
            return await _roleAppService.UpdateAsync(input);
        }
    }
}
