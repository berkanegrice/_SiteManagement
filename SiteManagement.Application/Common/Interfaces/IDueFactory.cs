using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.DueRelated;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;

namespace SiteManagement.Application.Common.Interfaces;

public interface IDueFactory
{
    Task<ResponseApplyDueListCommand> ApplyDueInfList(ApplyDueListRequest applyUserListRequest);
    Task<ResponseApplyDueListCommand> ApplyDueTransList(ApplyDueListRequest applyUserListRequest);
    Task<IQueryable<DueInformationDto>> GetAllDueInformation();
}