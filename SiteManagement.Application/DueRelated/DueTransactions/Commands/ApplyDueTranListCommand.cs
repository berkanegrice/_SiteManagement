using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.DueRelated;
using SiteManagement.Application.DueRelated.DueInformations.Command;

namespace SiteManagement.Application.DueRelated.DueTransactions.Commands;

public record ApplyDueTranListCommand
    : IRequest<ResponseApplyDueListCommand>
{
    public int Id { get; init; }
}

public class ApplyDueTranListCommandHandler
    : IRequestHandler<ApplyDueTranListCommand, ResponseApplyDueListCommand>
{
    private readonly IDueFactory _dueFactory;

    public ApplyDueTranListCommandHandler(IDueFactory dueFactory)
    {
        _dueFactory = dueFactory;
    }
    
    public async Task<ResponseApplyDueListCommand>
        Handle(ApplyDueTranListCommand request, CancellationToken cancellationToken)
    {
        return await _dueFactory.ApplyDueTransList(
            new ApplyDueListRequest()
            {
                Id = request.Id
            });
    }
}