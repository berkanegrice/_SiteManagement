using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Response;

namespace SiteManagement.Application.Common.Interfaces;

public interface IApplyService
{
    Task<ResponseApplyRegisterCommand> ApplyList(int requestId);
}