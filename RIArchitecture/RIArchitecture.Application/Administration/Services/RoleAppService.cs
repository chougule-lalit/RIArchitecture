using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Administration.Services
{
    public class RoleAppService : RIArchitectureAppService, IRoleAppService
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<RoleAppService> _logger;

        public RoleAppService(
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<RoleAppService> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public virtual async Task<PagedResultDto<RoleDto>> PagedResultAsync(GetRoleInputDto input)
        {
            var roles = await _roleManager.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            var count = roles.Count;
            var finalRolesList = roles.Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<RoleDto>
            {
                Items = finalRolesList,
                TotalCount = count
            };
        }

        public virtual async Task<bool> CreateAsync(string roleName)
        {
            if (roleName != null)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName.Trim()));
                return result.Succeeded;
            }

            return false;
        }

        public virtual async Task<bool> UpdateAsync(RoleDto input)
        {
            if (input != null)
            {
                var role = await _roleManager.FindByIdAsync(input.Id.ToString());
                if (role != null)
                {

                    role.Name = input.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    return result.Succeeded;
                }
                else
                {
                    _logger.LogError($"Role not found with Id : {input.Id}");
                    return false;
                }
            }
            else
            {
                _logger.LogError($"Input cannot be empty");
                return false;
            }
            
        }

        public virtual async Task<RoleDto> GetAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                return ObjectMapper.Map<IdentityRole<Guid>, RoleDto>(role);
            }
            else
            {
                _logger.LogError($"Role not found with Id : {id}");
                return new RoleDto();
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            else
            {
                _logger.LogError($"Role not found with Id : {id}");
                return false;
            }
        }

        public virtual async Task<bool> RoleExistAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
