using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.Interfaces
{
    public interface IUserAppService : IRIArchitectureAppService
    {
        Task<PagedResultDto<GetUserOutputDto>> GetPagedResultAsync(GetUserInputDto input);
        Task<Guid> CreateAsync(UserDto input);
        Task<bool> UpdateAsync(Guid id, UserDto input);
        Task<UserDto> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);

    }
}
