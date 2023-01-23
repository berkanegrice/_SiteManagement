using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.DueRelated.DueInformations.Response;

namespace SiteManagement.Application.Common.Interfaces;

public interface IDueFactory
{
    Task<ResponseApplyRegisterCommand> ApplyDueInfList(ApplyRegisterRequest applyUserListRequest);
    Task<ResponseApplyRegisterCommand> ApplyDueTransList(ApplyRegisterRequest applyUserListRequest);
    Task<IQueryable<DueInformationDto>> GetAllDueInformation();
}