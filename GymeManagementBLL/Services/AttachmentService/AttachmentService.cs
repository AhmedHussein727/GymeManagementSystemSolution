using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace GymeManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment webHost;
        public AttachmentService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        private readonly string[] AllowedExtentions = { ".jpg", ".jpeg", ".png" };
        private readonly long FileMaxSize = 5 * 1024 * 1024;

        public string? Upload(string FolderName, IFormFile file)
        {
            try
            {
                if (FolderName is null || file.Length == 0 || file is null) return null;

                if (file.Length > FileMaxSize) return null;

                var extention = Path.GetExtension(file.FileName).ToLower();

                if (!AllowedExtentions.Contains(extention)) return null;

                var folderPath = Path.Combine(webHost.WebRootPath, "images", FolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + extention;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to upload to folder name : {FolderName} : {ex}");
                return null;
            }

        }
        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName)||string.IsNullOrEmpty(folderName)) return false;

                var fullPath=Path.Combine(webHost.WebRootPath,"images",folderName,fileName);

                if(File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file : {fileName} : {ex}");
                return false;
            }
        }

       
    }
}
