using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;
using SiteManagement.Application.SufaRelated.SufaInformations.Queries;

namespace SiteManagement.Application.SufaRelated.Queries;

public record GetSufaInformationQuery : IRequest<IQueryable<SufaInformationDto>>
{
    public string? UserId { get; init; }
}

public class GetSufaInformationQueryHandler : IRequestHandler<GetSufaInformationQuery, IQueryable<SufaInformationDto>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetSufaInformationQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<IQueryable<SufaInformationDto>>
        Handle(GetSufaInformationQuery request, CancellationToken cancellationToken)
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
        
        var sufaInformationDto = 
            _context.DueInformations
                .Where(x => x.AccountCode > 1301000)
                .ProjectTo<SufaInformationDto>(_mapper.ConfigurationProvider);
        
        if (isSuperAdmin || isAdmin) 
            return sufaInformationDto;
        return sufaInformationDto.Where(x => x.Email == userEmail);

        #endregion
    }
}