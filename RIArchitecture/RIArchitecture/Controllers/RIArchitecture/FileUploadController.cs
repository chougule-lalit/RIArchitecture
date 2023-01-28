using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Shared;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.RIArchitecture
{
    [AllowAnonymous]
    public class FileUploadController : RIArchitectureBaseApiController
    {
        private readonly IFileUploadAppService _fileUploadAppService;

        public FileUploadController(IFileUploadAppService fileUploadAppService)
        {
            _fileUploadAppService = fileUploadAppService;
        }

        [HttpPost]
        [Route("fileUpload")]
        [AllowAnonymous]
        public async Task<IActionResult> FileUploadAsync([FromForm] FileUploadDto fileUpload)
        {
            var data = await _fileUploadAppService.FileUploadAsync(fileUpload);
            return Ok(new ApiWrapperResponseDto
            {
                ResponseMessage = data.IsSuccess ? ApiEnumResponse.DataFound.ToString() : ApiEnumResponse.DataNotFound.ToString(),
                ResponseValue = data.IsSuccess ? (int)ApiEnumResponse.DataFound : (int)ApiEnumResponse.DataNotFound,
                ResponseData = data
            });
        }

        [HttpPost]
        [Route("getFile")]
        public async Task<IActionResult> GetFileAsync(GetFileInputDto input)
        {

            var data = await _fileUploadAppService.GetFileAsync(input);
            return Ok(new ApiWrapperResponseDto
            {
                ResponseMessage = data.IsFileExist ? ApiEnumResponse.DataFound.ToString() : ApiEnumResponse.DataNotFound.ToString(),
                ResponseValue = data.IsFileExist ? (int)ApiEnumResponse.DataFound : (int)ApiEnumResponse.DataNotFound,
                ResponseData = data
            });
        }
    }
}
