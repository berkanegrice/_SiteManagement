using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Response;

namespace SiteManagement.Application.DueRelated.DueInformations.Command;

public record ApplyDueInfListCommand
    : IRequest<ResponseApplyRegisterCommand>
{
    public int Id { get; init; }
}

public class ApplyDueListCommandHandler
    : IRequestHandler<ApplyDueInfListCommand, ResponseApplyRegisterCommand>
{
    private readonly IDueFactory _dueFactory;

    public ApplyDueListCommandHandler(IDueFactory dueFactory)
    {
        _dueFactory = dueFactory;
    }
    
    public async Task<ResponseApplyRegisterCommand>
        Handle(ApplyDueInfListCommand request, CancellationToken cancellationToken)
    {
        return await _dueFactory.ApplyDueInfList(
            new ApplyRegisterRequest()
        {
            Id = request.Id
        });
    }
}