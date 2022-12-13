using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFile;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IMediator _mediator;
    
    public FileService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> UploadFile(UploadFileRequest request)
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
        var res = await _mediator.Send(
            new GetFileQuery()
            {
                Id = request.Id
            });
     
        
        
        throw new NotImplementedException();
    }
}