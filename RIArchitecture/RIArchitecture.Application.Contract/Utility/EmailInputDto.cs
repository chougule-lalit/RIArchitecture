using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class EmailInputDto
    {
        public List<string> Emails { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CCEmail { get; set; }
        public string BCCEmail { get; set; }
    }
}
