using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            Lists = from d in _context.DuesInformations
                join u in _context.UsersModel.Where(x => x.Email == currentUser.Email)
                    on d.AccountCode equals u.AccountCode
                    

        };

        // return new DuesInformationVm()
        // {
        //     Lists = await _context.DuesInformations
        //         .AsNoTracking()
        //         .ProjectTo<DuesInformationDto>(_mapper.ConfigurationProvider)
        //         .OrderBy(t => t.Title)
        //         .ToListAsync(cancellationToken)
        // };
    }
}


