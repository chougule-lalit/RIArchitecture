using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public interface IExcelSeederAppService: IRIArchitectureAppService
    {
        Task<int> GetAllAreaDataFromExcelAsync(FileUploadDto fileUpload);
    }
}
