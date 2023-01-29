using SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations;

namespace SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;

public class DueInformationVm
{
    public IQueryable<RegisterInformationDto>? Lists { get; init; }
    
}