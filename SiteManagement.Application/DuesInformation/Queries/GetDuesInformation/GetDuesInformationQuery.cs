using AutoMapper;
using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Security;

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
        return new DuesInformationVm()
        {
            
        }
    }
}

