using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Controllers.Administration
{
    [AllowAnonymous]
    public class EmailController : RIArchitectureBaseApiController, IEmailAppService
    {
        private readonly IEmailAppService _emailAppService;

        public EmailController(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        [HttpPost]
        [Route("sendEmail")]
        [AllowAnonymous]
        public async Task SendEmailAsync(EmailInputDto input)
        {
            await _emailAppService.SendEmailAsync(input);
        }
    }
}
