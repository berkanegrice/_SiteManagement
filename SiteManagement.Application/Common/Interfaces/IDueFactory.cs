using SiteManagement.Application.Common.Models;
using SiteManagement.Application.DueRelated.DueInformations.Command;

namespace SiteManagement.Application.Common.Interfaces;

public interface IDueFactory
{
    Task<ResponseApplyDueListCommand> ApplyDueInfList(ApplyDueListRequest applyUserListRequest);
    Task<ResponseApplyDueListCommand> ApplyDueTransList(ApplyDueListRequest applyUserListRequest);
}