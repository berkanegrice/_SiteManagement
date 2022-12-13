using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.Files.Queries.GetFiles;


public record GetFileQuery : IRequest<FileOnDataBaseDto>
{
    public int Id { get; init; }
}

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileOnDataBaseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileQueryHandler(IApplicationDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<FileOnDataBaseDto> Handle(GetFileQuery request, 
        CancellationToken cancellationToken)
    {
        //TODO : The file may not be found. Implement error handling mechanism.
        return await _context.FilesOnDatabase
            .Where(x => x.Id == request.Id)
            .ProjectTo<FileOnDataBaseDto>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}