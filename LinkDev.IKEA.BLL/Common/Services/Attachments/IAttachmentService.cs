using Microsoft.AspNetCore.Http;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        string Upload(IFormFile file, string folderName);
        bool Delete(string filePath);
    }
}
