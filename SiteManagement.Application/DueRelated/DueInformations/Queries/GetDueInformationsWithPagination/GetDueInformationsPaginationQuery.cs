using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Mappings;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Misc;
using SiteManagement.Application.Common.Security;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;

namespace SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformationsWithPagination;

[Authorize]
public record GetDuesInformationPaginationQuery : IRequest<PaginatedList<DueInformationDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetDuesInformationHandler : IRequestHandler<GetDuesInformationPaginationQuery, PaginatedList<DueInformationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    // private readonly IIdentityService _identityService;

    public GetDuesInformationHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        // _identityService = identityService;
    }
    
    public async Task<PaginatedList<DueInformationDto>> Handle(GetDuesInformationPaginationQuery request, CancellationToken cancellationToken)
    {

        return await _context.DueInformations
            .OrderBy(x => x.Id)
            .ProjectTo<DueInformationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize).ConfigureAwait(false);

        // return new DuesInformationVm()
        // {
        //     Lists = await _context.DuesInformations
        //         .AsNoTracking()
        //         .ProjectTo<DuesInformationDto>(_mapper.ConfigurationProvider)
        //         .ToListAsync(cancellationToken).ConfigureAwait(false)
        // };
    }
}