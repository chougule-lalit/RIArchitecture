using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class ApiWrapperResponseDto
    {
        public string ResponseMessage { get; set; }
        public int ResponseValue { get; set; }
        public object ResponseData { get; set; }
    }
}
