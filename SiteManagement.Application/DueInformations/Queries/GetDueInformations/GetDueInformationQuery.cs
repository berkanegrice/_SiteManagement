using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;

namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;


public record GetDueInformationQuery : IRequest<IQueryable<DueInformationDto>>
{
    public string? UserId { get; init; }
}

public class GetDueInformationHandler : IRequestHandler<GetDueInformationQuery, IQueryable<DueInformationDto>>
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
    
    public async Task<IQueryable<DueInformationDto>> Handle(GetDueInformationQuery request, 
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
        
        var dueInformationDto = 
            _context.DueInformations.ProjectTo<DueInformationDto>
                (_mapper.ConfigurationProvider);
        
        if (isSuperAdmin || isAdmin) 
            return dueInformationDto;
        return dueInformationDto.Where(x => x.Email == userEmail);

        #endregion
    }
}
