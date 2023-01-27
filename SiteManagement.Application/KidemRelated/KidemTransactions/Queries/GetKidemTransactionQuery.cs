using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Helper;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.KidemRelated.KidemTransactions.Queries;


public record GetKidemTransactionQuery : IRequest<IQueryable<KidemTransactionDto>>
{
    public int UserCode { get; init; }
}

public class GetKidemTransactionQueryHandler : IRequestHandler<GetKidemTransactionQuery, IQueryable<KidemTransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetKidemTransactionQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<IQueryable<KidemTransactionDto>>
        Handle(GetKidemTransactionQuery request, CancellationToken cancellationToken)
    {
        // #region Fetch Data
        //
        // // TODO: Fix Automapper
        //
        // return await Task.FromResult(_context
        //     .DueTransactions
        //     .Include(dt => dt.RegisterInformation)
        //     .Where(x => x.AccountCode == request.UserCode)
        //     .Select(s => new KidemTransactionDto()
        //     {
        //         Id = s.Id,
        //         Date = s.Date,
        //         Detail = s.Detail,
        //         Debt = s.Debt.ToDouble(),
        //         Credit = s.Credit.ToDouble(),
        //         BalanceDebt = s.BalanceDebt.ToDouble(),
        //         BalanceCredit = s.BalanceCredit.ToDouble()
        //     }));
        //
        // #endregion
        
        throw new NotImplementedException();
    }
}