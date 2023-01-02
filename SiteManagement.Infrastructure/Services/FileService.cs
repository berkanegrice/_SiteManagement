using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IMediator _mediator;
    
    public FileService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ResponseUploadFileCommand> UploadFile(UploadFileRequest request)
    {
        return await _mediator.Send(
            new UploadFileCommand()
            {
                UploadedBy = request.UploadedBy,
                File = request.FormFile, 
                Description = request.Description
            }
        );
    }

    public async Task<FileOnDataBaseDto> FetchFile(FetchFileRequest request)
    {
        return await _mediator.Send(
            new GetFileQuery()
            {
                Id = request.Id
            });
    }
}