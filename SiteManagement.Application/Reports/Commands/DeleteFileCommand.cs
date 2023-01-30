using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.File;

namespace SiteManagement.Application.Reports.Commands;

public record DeleteFileCommand
    : IRequest<ResponseDeleteFileCommand>
{
    public int Id { get; set; }
}

public class DeleteFileCommandHandler
    : IRequestHandler<DeleteFileCommand, ResponseDeleteFileCommand>
{
    private readonly IFileService _fileService;

    public DeleteFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<ResponseDeleteFileCommand>
        Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
       return await _fileService.DeleteFileById(
            new DeleteFileRequest()
        {
            Id = request.Id
        });
    }
}