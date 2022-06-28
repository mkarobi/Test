namespace TFWebService.Data.Dtos.Services
{
    public class FileUploadedDto
    {
        public bool Status { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; } = "0";
        public string Message { get; set; }
        public bool LocalUploaded { get; set; }

    }
}
