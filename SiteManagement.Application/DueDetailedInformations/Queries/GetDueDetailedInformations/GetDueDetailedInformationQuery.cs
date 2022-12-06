using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Domain.Entities.DuesRelated;

namespace SiteManagement.Application.DueDetailedInformations.Queries.GetDueDetailedInformations;

public record GetDueDetailedInformationQuery : IRequest<DueDetailedInformationVm>
{
    public int RowId { get; init; }
}

public class GetDueDetailedInformationHandler : IRequestHandler<GetDueDetailedInformationQuery, DueDetailedInformationVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetDueDetailedInformationHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<DueDetailedInformationVm> Handle(GetDueDetailedInformationQuery request, CancellationToken cancellationToken)
    {
        return new DueDetailedInformationVm()
        {
            // Lists = _mapper.Map<List<DueDetailedInformationsDto>>(
            //     await (
            //         from d in _context.DueInformations.Where(x=> x.DueInformationId.Equals(request.RowId))
            //         join dd in _context.DueDetailedInformations on d.DueInformationId equals dd.AccountCode
            //         select new DueTransaction()
            //         {
            //             Id = dd.Id,
            //             AccountCode = d.DueInformationId,
            //             Date = dd.Date,
            //             Detail = dd.Detail,
            //             Debt = dd.Debt,
            //             Credit = dd.Credit,
            //             BalanceDebt = dd.BalanceDebt,
            //             BalanceCredit = dd.BalanceCredit
            //         }
            //     ).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false)
            // )
        };
    }
}