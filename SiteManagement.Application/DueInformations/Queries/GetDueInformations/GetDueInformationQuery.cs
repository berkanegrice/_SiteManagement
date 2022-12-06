using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;

namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;


public record GetDueInformationQuery : IRequest<DueInformationVm>
{
    public string? UserId { get; init; }
}

public class GetDueInformationHandler : IRequestHandler<GetDueInformationQuery, DueInformationVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetDueInformationHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<DueInformationVm> Handle(GetDueInformationQuery request, CancellationToken cancellationToken)
    {
        var isSuperAdmin = await _identityService.IsInRoleAsync(request.UserId, ApplicationRoles.SuperAdmin.ToString())
            .ConfigureAwait(false);

        var isAdmin = await _identityService.IsInRoleAsync(request.UserId, ApplicationRoles.Admin.ToString())
            .ConfigureAwait(false);
        
        var userEmail = await _identityService.GetUserEmailAsync(request.UserId)
            .ConfigureAwait(false);

        if (isSuperAdmin || isAdmin)
        {
            // var res =await _context
            //     .Users
            //     .Include(user => user.Due)
            //     .ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            
            return new DueInformationVm()
            {
                Lists =  await _context.Users
                    .Include(user => user.Due)
                    .Select(d => new DueInformationDto()
                    {
                        LeaseHolder = d.UserName,
                        Debt = d.Due.Debt,
                        Credit = d.Due.Credit,
                        BalanceDebt = d.Due.BalanceDebt,
                        BalanceCredit = d.Due.BalanceCredit
                    })
                    .ToListAsync(cancellationToken: cancellationToken)
                    .ConfigureAwait(false)
            };
        }
        return new DueInformationVm()
        {
            Lists = _mapper.Map<List<DueInformationDto>>(
                await (
                    from d in _context.DueInformations
                    join u in _context.Users.Where(x => x.Email == userEmail)
                        on d.AccountCode equals u.UserCode
                    select new DueInformationDto()
                    {
                        LeaseHolder = u.UserName,
                        Debt = d.Debt,
                        Credit = d.Credit,
                        BalanceDebt = d.BalanceDebt,
                        BalanceCredit = d.BalanceCredit
                    }
                ).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false)
            )
        };
    }
}
