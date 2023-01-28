using Microsoft.EntityFrameworkCore;
using RIArchitecture.Application.Contracts;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Core.Entities;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using RIArchitecture.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application
{
    public class ItemAppService : RIArchitectureAppService, IItemAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _unitOfWork.Repository<Item>().Delete(id);
            var result = await _unitOfWork.Complete();
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<List<ItemDto>> GetAllItemsAync()
        {
            var query = await _unitOfWork.Repository<Item>().GetAll().ToListAsync();
            await _unitOfWork.Complete();
            return ObjectMapper.Map<List<Item>, List<ItemDto>>(query);
        }
    }
}
