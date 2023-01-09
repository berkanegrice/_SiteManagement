using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;
using SiteManagement.Application.Reports.Commands;

namespace SiteManagement.Infrastructure.Services.StorageServices;

public class FileService : IFileService
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _context;
    
    public FileService(IMediator mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
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

    public async Task<FileOnDataBaseDto> FetchFileById(FetchFileRequest request)
    {
        return await _mediator.Send(
            new GetFileQueryById()
            {
                Id = request.Id
            });
    }

    public async Task<IQueryable<FileOnDataBaseDto>> FetchFileByCategory(FetchFileRequest request)
    {
        return await _mediator.Send(
            new GetFileQueryByCategory()
            {
                Type = request.Type
            });
    }

    public async Task<ResponseDeleteFileCommand> DeleteFileById(DeleteFileRequest request)
    {
        return await _mediator.Send(
            new DeleteFileByIdCommand()
        {
            Id = request.Id
        });
    }
}
