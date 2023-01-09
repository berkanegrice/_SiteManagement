using System.Security.Claims;
using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Managements.Users.Queries.GetUserRoles;

namespace SiteManagement.Application.Managements.Users.Commands.UpdateUserRoles;

public record UpdateUserRolesCommand
    : IRequest<ResponseUpdateUserRolesCommand>
{
    public string Id { get; set; }
    public ManageUserRolesDto ManageUserRolesDto { get; set; }
    public ClaimsPrincipal User { get; set; }
}

public class UpdateUserRolesCommandHandler
    : IRequestHandler<UpdateUserRolesCommand, ResponseUpdateUserRolesCommand>
{
    private readonly IUserFactory _userFactory;
    private readonly ISignInFactory _signInFactory;

    public UpdateUserRolesCommandHandler(IUserFactory userFactory,
        ISignInFactory signInFactory)
    {
        _userFactory = userFactory;
        _signInFactory = signInFactory;
    }

    public async Task<ResponseUpdateUserRolesCommand>
        Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userFactory.FindByIdAsync(new FindByIdRequest() {UserId = request.Id});
        var roles = await _userFactory.GetRolesAsync(user);
        await _userFactory.RemoveFromRolesAsync(user, roles);
        await _userFactory.AddToRolesAsync(user,
            request.ManageUserRolesDto.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));

        var currentUser = await _userFactory.GetUserAsync(request.User);
        await _signInFactory.RefreshSignInAsync(currentUser);
        // await Seeds.DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager);

        return new ResponseUpdateUserRolesCommand()
        {
            Status = true
        };
    }
}