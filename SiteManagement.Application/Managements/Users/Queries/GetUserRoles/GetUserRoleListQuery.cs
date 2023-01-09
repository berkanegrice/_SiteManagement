using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;

namespace SiteManagement.Application.Managements.Users.Queries.GetUserRoles;

public record GetUserRoleListQuery : IRequest<ManageUserRolesDto>
{
    public string UserId { get; set; }
}

public class GetUserRoleListQueryHandler
    : IRequestHandler<GetUserRoleListQuery, ManageUserRolesDto>
{
    private readonly IUserFactory _userFactory;
    private readonly IRoleFactory _roleFactory;

    public GetUserRoleListQueryHandler(IUserFactory userFactory,
        IRoleFactory roleFactory)
    {
        _userFactory = userFactory;
        _roleFactory = roleFactory;
    }

    public async Task<ManageUserRolesDto> Handle(GetUserRoleListQuery request,
        CancellationToken cancellationToken)
    {
        var userRolesDtos = new List<UserRolesDto>();
        var user = await _userFactory.FindByIdAsync(new FindByIdRequest()
        {
            UserId = request.UserId
        });

        foreach (var role in _roleFactory.Roles.ToList())
        {
            var userRolesDto = new UserRolesDto {RoleName = role.Name};
            if (await _userFactory.IsInRoleAsync(new IsInRoleRequest()
                {
                    User = user, RoleName = role.Name
                }))
            {
                userRolesDto.Selected = true;
            }
            else
            {
                userRolesDto.Selected = false;
            }

            userRolesDtos.Add(userRolesDto);
        }

        return new ManageUserRolesDto()
        {
            UserId = request.UserId,
            UserRoles = userRolesDtos
        };
    }
}