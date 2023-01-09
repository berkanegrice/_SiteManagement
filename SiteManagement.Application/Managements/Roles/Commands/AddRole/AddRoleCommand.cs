using MediatR;
using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Application.Managements.Roles.Commands.AddRole;

public record AddRoleCommand : IRequest<ResponseAddRoleCommand>
{
    public string RoleName { get; set; }
}

public class AddRoleCommandHandler
    : IRequestHandler<AddRoleCommand, ResponseAddRoleCommand>
{
    private readonly IRoleFactory _roleFactory;

    public AddRoleCommandHandler(IRoleFactory roleFactory)
    {
        _roleFactory = roleFactory;
    }

    public async Task<ResponseAddRoleCommand> Handle(AddRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var resp = await _roleFactory.AddRole(request.RoleName);
        return new ResponseAddRoleCommand()
        {
            Status = resp.Succeeded
        };
    }
}