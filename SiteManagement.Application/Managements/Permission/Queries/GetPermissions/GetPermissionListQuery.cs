using MediatR;
using SiteManagement.Application.Common.Helper;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Constants;

namespace SiteManagement.Application.Managements.Permission.Queries.GetPermissions;

public record GetPermissionListQuery : IRequest<PermissionDto>
{
    public string RoleId { get; init; }
}

public class GetPermissionListQueryHandler
    : IRequestHandler<GetPermissionListQuery, PermissionDto>
{
    private readonly IRoleFactory _roleFactory;

    public GetPermissionListQueryHandler(IRoleFactory roleFactory)
    {
        _roleFactory = roleFactory;
    }
    
    public async Task<PermissionDto> Handle(GetPermissionListQuery request, 
        CancellationToken cancellationToken)
    {
        var allPermissions = new List<RoleClaimsDto>();
        var model = new PermissionDto();

        allPermissions.GetPermissions(typeof(ApplicationPermissions.Due), request.RoleId);
        
        var role = await _roleFactory.FindByIdAsync(request.RoleId);
        var claims = await _roleFactory.GetClaimsAsync(role);    
        var allClaimValues = allPermissions.Select(a => a.Value).ToList();
        var roleClaimValues = claims.Select(a => a.Value).ToList();
        var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
        foreach (var permission in allPermissions.Where(permission => authorizedClaims.Any(a => a == permission.Value)))
        {
            permission.Selected = true;
        }
        model.RoleClaims = allPermissions;

        return model;
    }
}