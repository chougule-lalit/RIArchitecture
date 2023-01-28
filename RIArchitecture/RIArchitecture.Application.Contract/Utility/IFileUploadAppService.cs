using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public interface IFileUploadAppService : IRIArchitectureAppService
    {
        Task<FileUploadOutputDto> FileUploadAsync(FileUploadDto fileUpload);
        Task<GetFileOutputDto> GetFileAsync(GetFileInputDto input);

    }
}
