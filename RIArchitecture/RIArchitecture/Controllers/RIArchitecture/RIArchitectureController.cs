using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIArchitecture.Application.Authorization;
using RIArchitecture.Application.Contracts;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Core.Entities;
using RIArchitecture.Infrastructure;
using RIArchitecture.Shared;
using System;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.PrismMason
{
    [Authorize]
    public class RIArchitectureController : RIArchitectureBaseApiController
    {
        private readonly RIArchitectureDbContext _context;
        private readonly IItemAppService _itemAppService;

        public RIArchitectureController(RIArchitectureDbContext context,
            IItemAppService itemAppService)
        {
            _context = context;
            _itemAppService = itemAppService;

        }

        [HttpGet]
        [Route("getItems")]
        [Authorize(Policy = Permissions.Items.View)]
        public async Task<IActionResult> GetItemAsync()
        {
            try
            {
                var data = await _itemAppService.GetAllItemsAync();

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = data.Count > 0 ? ApiEnumResponse.DataFound.ToString() : ApiEnumResponse.DataNotFound.ToString(),
                    ResponseValue = data.Count > 0 ? (int)ApiEnumResponse.DataFound : (int)ApiEnumResponse.DataNotFound,
                    ResponseData = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = ex.Message,
                    ResponseValue = (int)ApiEnumResponse.DataNotFound,
                    ResponseData = null
                };

                return Ok(result);
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = Permissions.Items.Create)]
        public async Task<IActionResult> CreateAsync(string name)
        {
            try
            {
                var data = new Item
                {
                    Name = name
                };

                await _context.Items.AddAsync(data);
                var ddd = await _context.SaveChangesAsync();

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = ddd > 0 ? ApiEnumResponse.DataFound.ToString() : ApiEnumResponse.DataNotFound.ToString(),
                    ResponseValue = ddd > 0 ? (int)ApiEnumResponse.DataFound : (int)ApiEnumResponse.DataNotFound,
                    ResponseData = ddd
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = ex.Message,
                    ResponseValue = (int)ApiEnumResponse.DataNotFound,
                    ResponseData = null
                };

                return Ok(result);
            }
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Policy = Permissions.Items.Edit)]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var result = await _context.Items
                .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.Name = "Some Test";

                await _context.SaveChangesAsync();

                return Ok(true);
            }

            return Ok(false);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Policy = Permissions.Items.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var data = await _itemAppService.DeleteAsync(id);

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = data ? ApiEnumResponse.DataFound.ToString() : ApiEnumResponse.DataNotFound.ToString(),
                    ResponseValue = data ? (int)ApiEnumResponse.DataFound : (int)ApiEnumResponse.DataNotFound,
                    ResponseData = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                var result = new ApiWrapperResponseDto
                {
                    ResponseMessage = ex.Message,
                    ResponseValue = (int)ApiEnumResponse.DataNotFound,
                    ResponseData = null
                };

                return Ok(result);
            }
            
        }
    }
}