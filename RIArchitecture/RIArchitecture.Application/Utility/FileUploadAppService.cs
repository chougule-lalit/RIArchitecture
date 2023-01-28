using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RIArchitecture.Application.Contracts.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RIArchitecture.Application.Utility
{
    public class FileUploadAppService : RIArchitectureAppService, IFileUploadAppService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<FileUploadAppService> _logger;

        public FileUploadAppService(IWebHostEnvironment env,
            ILogger<FileUploadAppService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task<FileUploadOutputDto> FileUploadAsync(FileUploadDto fileUpload)
        {
            try
            {
                if (fileUpload.File.Length > 0)
                {
                    var rootPath = _env.WebRootPath;
                    string path = $@"{rootPath}\{fileUpload.FileUploadFolder.ToString()}\";
                    var absoluteFilePath = path + fileUpload.File.FileName;
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    if (System.IO.File.Exists(absoluteFilePath))
                        return new FileUploadOutputDto
                        {
                            IsSuccess = false,
                            Message = $"{fileUpload.File.FileName} file already exists on path : {path}"
                        };

                    using (FileStream fileStream = System.IO.File.Create(absoluteFilePath))
                    {
                        var relativePath = $@"\{fileUpload.FileUploadFolder.ToString()}\{fileUpload.File.FileName}";
                        await fileUpload.File.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return new FileUploadOutputDto
                        {
                            IsSuccess = true,
                            Message = $"{fileUpload.File.FileName} file upload successfull",
                            Path = relativePath
                        };
                    }
                }
                else
                    return new FileUploadOutputDto
                    {
                        IsSuccess = false,
                        Message = "File upload failed"
                    };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Occured while Uploading file : {ex.Message}");
                return new FileUploadOutputDto
                {
                    IsSuccess = false,
                    Message = "File upload failed"
                };
            }
        }

        public async Task<GetFileOutputDto> GetFileAsync(GetFileInputDto input)
        {
            try
            {
                string path = $@"{_env.WebRootPath}\{input.FolderName}\{input.FileName}";
                if (System.IO.File.Exists(path))
                {
                    var provider = new FileExtensionContentTypeProvider();
                    string contentType = null;
                    provider.TryGetContentType(input.FileName, out contentType);
                    byte[] content = System.IO.File.ReadAllBytes(path);
                    return new GetFileOutputDto
                    {
                        Content = content,
                        ContentType = contentType,
                        IsFileExist = content.Length > 0,
                        FileName = input.FileName
                    };
                }
                else
                    return new GetFileOutputDto
                    {
                        IsFileExist = false
                    };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Occured while fetching file : {ex.Message}");
                return new GetFileOutputDto
                {
                    IsFileExist = false
                };
            }
        }
    }
}
