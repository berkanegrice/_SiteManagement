using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.User;

namespace SiteManagement.Application.Managements.Users.Commands.ApplyUser;

public record ApplyUserListCommand : IRequest<ResponseApplyUserListCommand>
{
    public int Id { get; init; }
}

public class ApplyUserListCommandHandler 
    : IRequestHandler<ApplyUserListCommand, ResponseApplyUserListCommand>
{
    private readonly IUserFactory _userFactory;

    public ApplyUserListCommandHandler(IUserFactory userFactory)
    {
        _userFactory = userFactory;
    }

    public async Task<ResponseApplyUserListCommand> Handle(
        ApplyUserListCommand request, CancellationToken cancellationToken)
    {
        return await _userFactory.ApplyUserList(new ApplyUserListRequest()
        {
            Id = request.Id
        });
    }
}