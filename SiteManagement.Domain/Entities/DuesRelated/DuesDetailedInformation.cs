namespace SiteManagement.Domain.Entities.DuesRelated;

public class DuesDetailedInformation : BaseAuditableEntity
{
    public int Id { get; set; }
    public string AccountCode { get; set; }
    public DateTime Date { get; set; }
    // public string Date { get; set; }
    public string Detail { get; set; }
    public string Debt { get; set; }
    public string Credit { get; set; }
    public string BalanceDebt { get; set; }
    public string BalanceCredit { get; set; }
}