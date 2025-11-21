using Microsoft.AspNetCore.Http;



namespace GymeManagementBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload(string FolderName, IFormFile file);
        bool Delete(string fileName,string folderName);
    }
}
