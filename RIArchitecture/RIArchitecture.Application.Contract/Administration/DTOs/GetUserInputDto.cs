using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.DTOs
{
    public class GetUserInputDto : PagedResultRequestDto
    {
        public List<string> RoleNames { get; set; }
    }
}
