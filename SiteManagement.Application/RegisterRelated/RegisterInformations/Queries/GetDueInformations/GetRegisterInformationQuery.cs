using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;
using AutoMapper;
using MediatR;

namespace SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations;

public record GetRegisterInformationQuery : IRequest<IQueryable<RegisterInformationDto>>
{
    public string? UserId { get; init; }
    public string Type { get; set; }
}

public class GetDueInformationHandler : IRequestHandler<GetRegisterInformationQuery, IQueryable<RegisterInformationDto>>
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
    
    public async Task<IQueryable<RegisterInformationDto>> Handle(GetRegisterInformationQuery request, 
        CancellationToken cancellationToken)
    {
        #region Detect Roles
        
        var isSuperAdmin = await _identityService.IsInRoleAsync(request.UserId, 
                ApplicationRoles.SuperAdmin.ToString())
            .ConfigureAwait(false);
        
        var isAdmin = await _identityService.IsInRoleAsync(request.UserId, 
                ApplicationRoles.Admin.ToString())
            .ConfigureAwait(false);
        
        var userEmail = await _identityService.GetUserEmailAsync(request.UserId)
            .ConfigureAwait(false);
        
        #endregion
        
        #region Fetch Data
        
        // TODO: Fix Automapper for RegisterInformation.
        var query =
            from users in _context.Users
            join ri in _context.RegisterInformations on users.AccountCode equals ri.AccountCode
            where users.Type == request.Type
            select new RegisterInformationDto()
            {
                Id = ri.Id,
                LeaseHolder = users.UserName,
                Email = users.Email,
                AccountCode = ri.AccountCode,
                Credit = ri.Credit,
                Debt = ri.Debt,
                BalanceCredit = ri.BalanceCredit,
                BalanceDebt = ri.BalanceDebt
            };
        
        #endregion

        if (isSuperAdmin || isAdmin) 
            return query;
        return query.Where(x => x.Email == userEmail);
        
        throw new NotImplementedException();

    }
}
