using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Helper;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.SufaRelated.SufaTransactions.Queries;


public record GetSufaTransactionQuery : IRequest<IQueryable<SufaTransactionDto>>
{
    public int UserCode { get; init; }
}

public class GetSufaTransactionQueryHandler : IRequestHandler<GetSufaTransactionQuery, IQueryable<SufaTransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetSufaTransactionQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<IQueryable<SufaTransactionDto>>
        Handle(GetSufaTransactionQuery request, CancellationToken cancellationToken)
    {
        #region Fetch Data
        
        // TODO: Fix Automapper for DueTransaction.

        return await Task.FromResult(_context
            .DueTransactions
            .Include(dt => dt.DueInformation)
            .Where(x => x.AccountCode == request.UserCode)
            .Select(s => new SufaTransactionDto()
            {
                Id = s.Id,
                Date = s.Date,
                Detail = s.Detail,
                Debt = s.Debt.ToDouble(),
                Credit = s.Credit.ToDouble(),
                BalanceDebt = s.BalanceDebt.ToDouble(),
                BalanceCredit = s.BalanceCredit.ToDouble()
            }));

        #endregion
    }
}