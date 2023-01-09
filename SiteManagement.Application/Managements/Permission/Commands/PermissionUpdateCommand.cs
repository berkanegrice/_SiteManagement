using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Managements.Permission.Queries.GetPermissions;

namespace SiteManagement.Application.Managements.Permission.Commands;


public record PermissionUpdateCommand : IRequest<ResponsePermissionUpdateCommand>
{
    public PermissionDto PermissionDto { get; set; }
} 

public class PermissionUpdateCommandHandler : IRequestHandler<PermissionUpdateCommand, ResponsePermissionUpdateCommand>
{
    private readonly IRoleFactory _roleFactory;

    public PermissionUpdateCommandHandler(IRoleFactory roleFactory)
    {
        _roleFactory = roleFactory;
    }
    
    public async Task<ResponsePermissionUpdateCommand> Handle(PermissionUpdateCommand request, 
        CancellationToken cancellationToken)
    {
        var model = request.PermissionDto;
        
        var role = await _roleFactory.FindByIdAsync(model.RoleId);
        var claims = await _roleFactory.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleFactory.RemoveClaimAsync(role, claim);
        }        
        var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleFactory.AddPermissionClaim(role, claim.Value);
        }

        return new ResponsePermissionUpdateCommand()
        {
            Status = true
        };
    }
}