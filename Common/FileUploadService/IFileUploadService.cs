namespace EduReg.Common.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadToServer(IFormFile fileName, string folder);
        Task<string> UploadToServer(IFormFile fileName, string folder, string subFolder);
    }
}
