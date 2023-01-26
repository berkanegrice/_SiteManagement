using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.DueRelated.DueInformations.Response;

namespace SiteManagement.Application.Common.Interfaces.Due;

public interface IRegisterFactory
{
    Task<ResponseApplyRegisterCommand> ApplyRegisterInfList(ApplyRegisterRequest request);
    Task<ResponseApplyRegisterCommand> ApplyRegisterTransList(ApplyRegisterRequest request);
    Task<IQueryable<DueInformationDto>> GetAllDueInformation();
}