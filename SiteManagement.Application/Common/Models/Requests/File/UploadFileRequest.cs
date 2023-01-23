using Microsoft.AspNetCore.Http;

namespace SiteManagement.Application.Common.Models.Requests.File;

public class UploadFileRequest
{
    public IFormFile File { get; set; }
    public string Description { get; set; }
    public string FileType { get; set; }
    public string UploadedBy { get; set; }
}