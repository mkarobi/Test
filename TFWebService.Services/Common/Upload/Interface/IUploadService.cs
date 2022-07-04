using TFWebService.Data.Dtos.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFWebService.Common.Helper;

namespace TFWebService.Services.Upload.Interface
{
    public interface IUploadService
    {
        Task<FileUploadedDto> UploadPic(IFormFile file, string userId, string WebRootPath, string UrlBegan, EnumCategoryFilesPath categoryPath);
        FileUploadedDto RemoveFileFromLocal(string photoName, string WebRootPath, string filePath);
    }
}
 