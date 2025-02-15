using _0_Framework.Application;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return string.Empty;

            var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "ProductPicture", path);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = $"{DateTime.Now.ToFileTime()}_{file.FileName}";
            var filePath = $"{directoryPath}\\{fileName}";
            using (var output = File.Create(filePath))
            {
                file.CopyTo(output);
            }
            return Path.Combine(path, fileName);
        }
    }
}
