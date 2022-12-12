using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.DueTransactions.Queries.GetDueTransactions;

public record GetDueTransactionQuery : IRequest<IQueryable<DueTransactionDto>>
{
    public int UserCode { get; init; }
}

public class GetDueTransactionHandler : IRequestHandler<GetDueTransactionQuery, IQueryable<DueTransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetDueTransactionHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public Task<IQueryable<DueTransactionDto>> Handle(GetDueTransactionQuery request, 
        CancellationToken cancellationToken)
    {
        #region Fetch Data
        
        // TODO: Fix Automapper for DueTransaction.
        // return Task.FromResult(
        //         _context.DueInformations.
        //             Where(x => x.AccountCode == request.UserCode)
        //             .ProjectTo<DueTransactionDto>(_mapper.ConfigurationProvider));

        return Task.FromResult(_context
            .DueTransactions
            .Include(dt => dt.DueInformation)
            .Where(x => x.AccountCode == request.UserCode)
            .Select(s => new DueTransactionDto()
            {
                Id = s.Id,
                Date = s.Date,
                Detail = s.Detail,
                Debt = s.Debt,
                Credit = s.Credit,
                BalanceDebt = s.BalanceDebt,
                BalanceCredit = s.BalanceCredit
            }));

        #endregion
    }
}