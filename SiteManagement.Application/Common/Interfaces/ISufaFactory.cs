using SiteManagement.Application.Common.Models;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.SufaRelated.Queries;
using SiteManagement.Application.SufaRelated.SufaInformations.Commands;
using SiteManagement.Application.SufaRelated.SufaInformations.Queries;

namespace SiteManagement.Application.Common.Interfaces;

public interface ISufaFactory
{
    Task<ResponseApplySufaListCommand> ApplySufaInfList(ApplySufaListRequest applyUserListRequest);
    Task<ResponseApplySufaListCommand> ApplySufaTransList(ApplySufaListRequest applyUserListRequest);
    Task<IQueryable<SufaInformationDto>> GetAllSufaInformation();
}