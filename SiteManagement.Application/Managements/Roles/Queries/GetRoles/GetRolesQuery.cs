using MediatR;
using Microsoft.AspNetCore.Identity;

namespace SiteManagement.Application.Managements.Roles.Queries.GetRoles;

public record GetRolesQuery : IRequest<IQueryable<IdentityRole>>
{
    public string? UserId { get; init; }
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IQueryable<IdentityRole>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task<IQueryable<IdentityRole>>
        Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_roleManager.Roles);
    }
}