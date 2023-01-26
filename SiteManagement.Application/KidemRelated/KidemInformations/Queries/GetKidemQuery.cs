using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;

namespace SiteManagement.Application.KidemRelated.KidemInformations.Queries;

public record GetKidemInformationQuery : IRequest<IQueryable<KidemInformationDto>>
{
    public string? UserId { get; init; }
}

public class GetKidemInformationQueryHandler : IRequestHandler<GetKidemInformationQuery, IQueryable<KidemInformationDto>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetKidemInformationQueryHandler(IApplicationDbContext context, IMapper mapper,
        IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<IQueryable<KidemInformationDto>>
        Handle(GetKidemInformationQuery request, CancellationToken cancellationToken)
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
                .Where(x => x.AccountCode > 13301000)
                .ProjectTo<KidemInformationDto>(_mapper.ConfigurationProvider);

        if (isSuperAdmin || isAdmin)
            return sufaInformationDto;
        return sufaInformationDto.Where(x => x.Email == userEmail);

        #endregion
    }
}