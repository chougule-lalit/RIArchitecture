using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class FileUploadOutputDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
    }
}
