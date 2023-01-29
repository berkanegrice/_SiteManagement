using MediatR;
using Microsoft.AspNetCore.Http;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.File;


namespace SiteManagement.Application.Files.Commands.UploadFiles;


public record UploadFileCommand : IRequest<ResponseUploadFileCommand>
{  
    public IFormFile File { get; init; }
    public string Description { get; init; }
    public string FileType { get; init; }
    public string UploadedBy { get; init; }
}

public class UploadFileCommandHandler
    : IRequestHandler<UploadFileCommand, ResponseUploadFileCommand>
{
    private readonly IFileService _fileService;
    
    public UploadFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<ResponseUploadFileCommand> Handle(UploadFileCommand request, 
        CancellationToken cancellationToken)
    {
        return await _fileService.UploadFile(new UploadFileRequest()
        {
            File = request.File,
            Description = request.Description,
            UploadedBy = request.UploadedBy,
            FileType = request.FileType
        });
    }
}