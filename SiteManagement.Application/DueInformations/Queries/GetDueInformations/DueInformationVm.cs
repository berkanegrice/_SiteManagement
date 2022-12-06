namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;

public class DueInformationVm
{
    public IList<DueInformationDto> Lists { get; init; } = new List<DueInformationDto>();
}