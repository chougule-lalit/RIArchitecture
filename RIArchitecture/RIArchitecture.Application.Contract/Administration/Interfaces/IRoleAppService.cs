using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.Interfaces
{
    public interface IRoleAppService : IRIArchitectureAppService
    {
        Task<PagedResultDto<RoleDto>> PagedResultAsync(GetRoleInputDto input);
        Task<bool> CreateAsync(string roleName);
        Task<bool> UpdateAsync(RoleDto input);
        Task<RoleDto> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> RoleExistAsync(string roleName);
    }
}
