using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Application.Reports.Commands;

public record DownloadFileCommand : IRequest<FileOnDataBaseDto>
{
    public int Id { get; set; }
}

public class DownloadFileCommandHandler
    : IRequestHandler<DownloadFileCommand, FileOnDataBaseDto>
{
    private readonly IFileService _fileService;

    public DownloadFileCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<FileOnDataBaseDto>
        Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        return await _fileService.FetchFileById(new FetchFileRequest()
        {
            Id = request.Id
        });
    }
}