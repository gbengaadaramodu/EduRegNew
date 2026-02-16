namespace EduReg.Common.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadService"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public FileUploadService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadToServer(IFormFile fileName, string folder)
        {
            if (fileName == null) return string.Empty;

            // Get the wwwroot path
            string wwwrootPath = env.WebRootPath;

            string documentPath = folder;
            string dirPath = Path.Combine(wwwrootPath, documentPath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string uniqueFileName = $"{DateTime.Now.Ticks.ToString()}{Path.GetExtension(fileName.FileName)}";
            string path = Path.Combine(dirPath, uniqueFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            await fileName.CopyToAsync(stream);
            stream.Close();

            // Construct the URL based on the current request context
            string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            string fileUrl = $"{baseUrl}/{documentPath}/{uniqueFileName}";

            return fileUrl;
        }


        public async Task<string> UploadToServer(IFormFile fileName, string folder, string subFolder)
        {
            if (fileName == null) return string.Empty;

            // Get the wwwroot path
            string wwwrootPath = env.WebRootPath;

            string documentPath = $"{folder}/{subFolder}";
            string dirPath = Path.Combine(wwwrootPath, documentPath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string uniqueFileName = $"{DateTime.Now.Ticks.ToString()}{Path.GetExtension(fileName.FileName)}";
            uniqueFileName = fileName.FileName + " " + uniqueFileName;
            string path = Path.Combine(dirPath, uniqueFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            await fileName.CopyToAsync(stream);
            stream.Close();

            // Construct the URL based on the current request context
            string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            string fileUrl = $"{baseUrl}/{documentPath}/{uniqueFileName}";

            return fileUrl;
        }
    }
}