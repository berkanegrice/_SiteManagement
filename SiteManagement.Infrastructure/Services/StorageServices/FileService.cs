using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Files.Commands.UploadFiles;
using SiteManagement.Application.Files.Queries.GetFiles;
using SiteManagement.Application.Reports.Commands;
using SiteManagement.Domain.Entities.FileRelated;
using SiteManagement.Domain.Events;

namespace SiteManagement.Infrastructure.Services.StorageServices;

public class FileService : IFileService
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public FileService(IMediator mediator, IApplicationDbContext context, IMapper mapper)
    {
        _mediator = mediator;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseUploadFileCommand> UploadFile(UploadFileRequest request)
    {
        var fileName = Path.GetFileNameWithoutExtension(request.File.FileName);
        var extension = Path.GetExtension(request.File.FileName);
        var fileModel = new FileOnDatabaseModel
        {
            Name = fileName,
            Description = request.Description,
            FileType = request.FileType,
            Extension = extension,
            UploadedBy = request.UploadedBy,
            CreatedOn = DateTime.UtcNow
        };
        
        using (var dataStream = new MemoryStream())
        {
            await request.File.CopyToAsync(dataStream).ConfigureAwait(false);
            fileModel.Data = dataStream.ToArray();
        }
        
        fileModel.AddDomainEvent(new NewUserListAddedEvent(fileModel));
        _context.FilesOnDatabase.Add(fileModel);

        return new ResponseUploadFileCommand()
        {
            Status = await _context.SaveChangesAsync(default).
                ConfigureAwait(false) > 0,
            InsertedId = fileModel.Id
        };
    }

    public async Task<FileOnDataBaseDto> FetchFileById(FetchFileRequest request)
    {
        //TODO : The file may not be found. Implement error handling mechanism.
        return await _context.FilesOnDatabase
            .Where(x => x.Id == request.Id)
            .ProjectTo<FileOnDataBaseDto>(_mapper.ConfigurationProvider)
            .FirstAsync(default).ConfigureAwait(false);
    }

    public async Task<IQueryable<FileOnDataBaseDto>> FetchFileByCategory(FetchFileRequest request)
    {
        return await 
            Task.FromResult(_context.FilesOnDatabase
                .Where(x => x.FileType.Equals(request.Type))
                .ProjectTo<FileOnDataBaseDto>(_mapper.ConfigurationProvider));
    }

    public async Task<ResponseDeleteFileCommand> DeleteFileById(DeleteFileRequest request)
    {
        var file = await _context.FilesOnDatabase
            .Where(x => x.Id == request.Id)
            .FirstAsync(default);
        
        _context.FilesOnDatabase.Remove(file);
        
        return new ResponseDeleteFileCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0
        };
    }
}
