using Microsoft.AspNetCore.Http;

namespace SiteManagement.Application.Common.Models;

public class UploadFileRequest
{
    public IFormFile FormFile { get; set; }
    public string Description { get; set; }
    public string UploadedBy { get; set; }
}