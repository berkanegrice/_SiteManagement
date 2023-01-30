using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Interfaces.Due;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Response;

namespace SiteManagement.Application.DueRelated.DueTransactions.Commands;

public record ApplyDueTranListCommand
    : IRequest<ResponseApplyRegisterCommand>
{
    public int Id { get; init; }
}

public class ApplyDueTranListCommandHandler
    : IRequestHandler<ApplyDueTranListCommand, ResponseApplyRegisterCommand>
{
    private readonly IRegisterFactory _registerFactory;

    public ApplyDueTranListCommandHandler(IRegisterFactory registerFactory)
    {
        _registerFactory = registerFactory;
    }
    
    public async Task<ResponseApplyRegisterCommand>
        Handle(ApplyDueTranListCommand request, CancellationToken cancellationToken)
    {
        return await _registerFactory.ApplyRegisterTransList(
            new ApplyRegisterRequest()
            {
                Id = request.Id
            });
    }
}