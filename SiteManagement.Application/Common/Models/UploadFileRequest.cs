
using Microsoft.AspNetCore.Http;

namespace SiteManagement.Application.Common.Models;

public class UploadFileRequest
{
    public string Description { get; set; }
    public string UploadedBy { get; set; }
    public IFormFile FormFile { get; set; }
}