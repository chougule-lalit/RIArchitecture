using RIArchitecture.Application.Contracts.Utility.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.Interfaces
{
    public interface IUserRoleAppService
    {
        Task<ManageUserRolesDto> GetUserRoleAsync(string userId);

        Task<bool> Update(ManageUserRolesDto model);
    }
}
