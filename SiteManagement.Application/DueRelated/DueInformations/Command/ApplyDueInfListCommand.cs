using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;

namespace SiteManagement.Application.DueRelated.DueInformations.Command;

public record ApplyDueInfListCommand
    : IRequest<ResponseApplyDueListCommand>
{
    public int Id { get; init; }
}

public class ApplyDueListCommandHandler
    : IRequestHandler<ApplyDueInfListCommand, ResponseApplyDueListCommand>
{
    private readonly IDueFactory _dueFactory;

    public ApplyDueListCommandHandler(IDueFactory dueFactory)
    {
        _dueFactory = dueFactory;
    }
    
    public async Task<ResponseApplyDueListCommand>
        Handle(ApplyDueInfListCommand request, CancellationToken cancellationToken)
    {
        return await _dueFactory.ApplyDueInfList(
            new ApplyDueListRequest()
        {
            Id = request.Id
        });
    }
}