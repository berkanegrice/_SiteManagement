using MediatR;
using Microsoft.AspNetCore.Http;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Domain.Entities.FileRelated;
using SiteManagement.Domain.Events;

namespace SiteManagement.Application.Files.Commands.UploadFiles;


public record UploadFileCommand : IRequest<ResponseUploadFileCommand>
{  
    public string UploadedBy { get; init; }
    public IFormFile File { get; init; }
    public string Description { get; init; }
    
    public FileCategory FileCategory { get; set; }
}

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ResponseUploadFileCommand>
{
    private readonly IApplicationDbContext _context;
    
    public UploadFileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseUploadFileCommand> Handle(UploadFileCommand request, 
        CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileNameWithoutExtension(request.File.FileName);
        var extension = Path.GetExtension(request.File.FileName);
        var fileModel = new FileOnDatabaseModel
        {
            CreatedOn = DateTime.UtcNow,
            FileType = request.File.ContentType,
            Extension = extension,
            UploadedBy = request.UploadedBy,
            Name = fileName,
            Description = request.Description
        };
        
        using (var dataStream = new MemoryStream())
        {
            await request.File.CopyToAsync(dataStream, cancellationToken).ConfigureAwait(false);
            fileModel.Data = dataStream.ToArray();
        }
        
        fileModel.AddDomainEvent(new NewUserListAddedEvent(fileModel));
        _context.FilesOnDatabase.Add(fileModel);

        return new ResponseUploadFileCommand()
        {
            Success = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0,
            InsertedId = fileModel.Id
        };
    }
}