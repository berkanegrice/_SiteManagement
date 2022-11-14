using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Security;
using SiteManagement.Domain.Entities;

namespace SiteManagement.Application.DuesInformation.Queries.GetDuesInformation;

[Authorize]
public record GetDuesInformationQuery : IRequest<DuesInformationVm>;

public class GetDuesInformationHandler : IRequestHandler<GetDuesInformationQuery, DuesInformationVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDuesInformationHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DuesInformationVm> Handle(GetDuesInformationQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.DuesInformations
            .AsNoTracking()
            .ProjectTo<DuesInformationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        // Lists = await _context.DuesInformations
        //     .AsNoTracking()
        //     .ProjectTo<DuesInformationDto>(_mapper.ConfigurationProvider)
        //     .ToListAsync(cancellationToken).ConfigureAwait(false)
            
        return new DuesInformationVm()
        {
            Lists = list
        };
    }
}


