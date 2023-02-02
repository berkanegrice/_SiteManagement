using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Interfaces.Due;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Response;

namespace SiteManagement.Application.DueRelated.DueInformations.Command;

public record ApplyDueInfListCommand
    : IRequest<ResponseApplyRegisterCommand>
{
    public int Id { get; init; }
}

public class ApplyDueListCommandHandler
    : IRequestHandler<ApplyDueInfListCommand, ResponseApplyRegisterCommand>
{
    private readonly IRegisterFactory _registerFactory;

    public ApplyDueListCommandHandler(IRegisterFactory registerFactory)
    {
        _registerFactory = registerFactory;
    }
    
    public async Task<ResponseApplyRegisterCommand>
        Handle(ApplyDueInfListCommand request, CancellationToken cancellationToken)
    {
        return await _registerFactory.ApplyRegisterInfList(
            new ApplyRegisterRequest()
        {
            Id = request.Id
        });
    }
}