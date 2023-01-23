using SiteManagement.Application.DueRelated.DueInformations.Response;

namespace SiteManagement.Application.Common.Interfaces;

public interface IApplyService
{
    Task<ResponseApplyRegisterCommand> ApplyList(int requestId);
}