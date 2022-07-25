using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Services;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Upload.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using TFWebService.Common.Helper;

namespace TFWebService.Services.Upload.Service
{
    public class UploadService : IUploadService
    {
        private readonly IUnitOfWork<TFDbContext> _db;
        public UploadService(IUnitOfWork<TFDbContext> dbContext)
        {
            _db = dbContext;
        }
        public async Task<FileUploadedDto> UploadPic(IFormFile file, string userId, string WebRootPath, string UrlBegan,EnumCategoryFilesPath categoryPath)
        {
            
            return await UploadPicToLocal(file, userId, WebRootPath, UrlBegan, categoryPath);
            
        }

        public async Task<FileUploadedDto> UploadPicToLocal(IFormFile file, string userId, string WebRootPath, string UrlBegan, EnumCategoryFilesPath categoryPath)
        {

            if (file.Length > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtention = Path.GetExtension(fileName);
                    string fileNewName = string.Format("-{0}-{1}{2}", userId, DateTime.Now.ToString().Replace("/","").Replace(":",""), fileExtention).Replace(" ","-");
                    string path = Path.Combine(WebRootPath, "Files\\Pic\\"+categoryPath);
                    string fullPath = Path.Combine(path, fileNewName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return new FileUploadedDto()
                    {
                        Status = true,
                        LocalUploaded = true,
                        Message = "با موفقیت در لوکال آپلود شد",
                        PublicId = "0",
                        Url = string.Format("{0}/{1}", UrlBegan, "wwwroot/Files/Pic/"+ categoryPath + fileNewName)
                    };
                }
                catch (Exception ex)
                {
                    return new FileUploadedDto()
                    {
                        Status = false,
                        Message = ex.Message
                    };
                }
            }
            else
            {
                return new FileUploadedDto()
                {
                    Status = false,
                    Message = "فایلی برای اپلود یافت نشد"
                };
            }
        }

        public FileUploadedDto RemoveFileFromLocal(string photoName, string WebRootPath, string filePath)
        {

            string path = Path.Combine(WebRootPath, filePath);
            string fullPath = Path.Combine(path, photoName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return new FileUploadedDto()
                {
                    Status = true,
                    Message = "فایل با موفقیت حذف شد"
                };
            }
            else
            {
                return new FileUploadedDto()
                {
                    Status = true,
                    Message = "فایل وجود نداشت"
                };
            }
        }
    }
}
