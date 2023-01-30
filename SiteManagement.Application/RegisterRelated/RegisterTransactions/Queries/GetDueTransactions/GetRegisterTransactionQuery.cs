using AutoMapper;
using MediatR;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;

public record GetRegisterTransactionQuery : IRequest<IQueryable<RegisterTransactionDto>>
{
    public int UserCode { get; init; }
}

public class GetDueTransactionHandler : IRequestHandler<GetRegisterTransactionQuery, IQueryable<RegisterTransactionDto>>
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
    
    public Task<IQueryable<RegisterTransactionDto>> Handle(GetRegisterTransactionQuery request, 
        CancellationToken cancellationToken)
    {
        #region Fetch Data
        
        // TODO: Fix Automapper for DueTransaction.

        var query = Task.FromResult( 
            from rt in _context.RegisterTransactions
            where rt.AccountCode == request.UserCode
            select new RegisterTransactionDto()
            {
                Id = rt.Id,
                Date = rt.Date,
                Detail = rt.Detail,
                Credit = rt.Credit,
                Debt = rt.Debt,
                BalanceDebt = rt.BalanceDebt,
                BalanceCredit = rt.BalanceCredit,
            }); 
        
        #endregion

        return query;
    }
}