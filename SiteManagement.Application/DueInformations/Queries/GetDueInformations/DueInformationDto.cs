namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;
public class DueInformationDto
{
    public DueInformationDto()
    {
        DueInformations = new List<DueInformationDto>();
    }
    public string LeaseHolder { get; set; }
    public string? Debt { get; set; }
    public string? Credit { get; set; }
    public string? BalanceDebt { get; set; }
    public string? BalanceCredit { get; set; }
    private IList<DueInformationDto> DueInformations { get; set; }
}