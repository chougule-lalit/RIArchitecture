using Microsoft.AspNetCore.Http;
using RIArchitecture.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }

        public FileUploadFolderDetail FileUploadFolder { get; set; }
    }
}
