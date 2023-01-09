using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.Reports.Commands;


public record DeleteFileByIdCommand : IRequest<ResponseDeleteFileCommand>
{
    public int Id { get; set; }
}

public class DeleteFileByIdCommandHandler
    : IRequestHandler<DeleteFileByIdCommand, ResponseDeleteFileCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteFileByIdCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseDeleteFileCommand>
        Handle(DeleteFileByIdCommand request, CancellationToken cancellationToken)
    {
        var file = await _context.FilesOnDatabase
            .Where(x => x.Id == request.Id)
            .FirstAsync(cancellationToken: cancellationToken);
        
        _context.FilesOnDatabase.Remove(file);
        
        return new ResponseDeleteFileCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0
        };
    }
}