using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts
{
    public interface IItemAppService : IRIArchitectureAppService
    {
        Task<bool> DeleteAsync(int id);

        Task<List<ItemDto>> GetAllItemsAync();
    }
}
