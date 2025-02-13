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

        public string Upload(IFormFile file)
        {
            if (file == null) return string.Empty;
            var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{file.FileName}";
            using (var output = System.IO.File.Create(path))
            {
                file.CopyTo(output);
            }

        }
    }
}
