using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.Files.Queries.GetFiles;


public record GetFileQueryByCategory : IRequest<IQueryable<FileOnDataBaseDto>>
{
    public string Type { get; set; }
}

public class GetFileQueryByCategoryHandler
    : IRequestHandler<GetFileQueryByCategory, IQueryable<FileOnDataBaseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileQueryByCategoryHandler(IApplicationDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IQueryable<FileOnDataBaseDto>> Handle(GetFileQueryByCategory request, 
        CancellationToken cancellationToken)
    {
        return await 
            Task.FromResult(_context.FilesOnDatabase
                .Where(x => x.Description!.StartsWith(request.Type))
                .ProjectTo<FileOnDataBaseDto>(_mapper.ConfigurationProvider));
    }
}