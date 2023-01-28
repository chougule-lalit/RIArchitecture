using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.Utility
{
    [AllowAnonymous]
    public class ExcelSeederController : RIArchitectureBaseApiController
    {
        private readonly IExcelSeederAppService _excelSeederAppService;

        public ExcelSeederController(IExcelSeederAppService excelSeederAppService)
        {
            _excelSeederAppService = excelSeederAppService;
        }

        [HttpPost]
        [Route("getAllAreaDataFromExcel")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAreaDataFromExcelAsync([FromForm] FileUploadDto fileUpload)
        {
            return Ok(await _excelSeederAppService.GetAllAreaDataFromExcelAsync(fileUpload));
        }
    }
}
