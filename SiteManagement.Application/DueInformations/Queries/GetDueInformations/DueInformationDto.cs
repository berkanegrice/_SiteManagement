namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;
public class DueInformationDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int AccountCode { get; set; }
    public string LeaseHolder { get; set; }
    public string Debt { get; set; }
    public string Credit { get; set; }
    public string BalanceDebt { get; set; }
    public string BalanceCredit { get; set; }
}