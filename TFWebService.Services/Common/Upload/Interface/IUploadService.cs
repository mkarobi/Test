using TFWebService.Data.Dtos.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFWebService.Services.Upload.Interface
{
    public interface IUploadService
    {
        Task<FileUploadedDto> UploadPic(IFormFile file, string userId, string WebRootPath, string UrlBegan);
        FileUploadedDto RemoveFileFromLocal(string photoName, string WebRootPath, string filePath);
    }
}
 