namespace SiteManagement.Domain.Entities.DuesRelated;

public class DuesInformation : BaseAuditableEntity
{
    public int Id { get; set; }
    public string AccountCode { get; set; }
    public string Debt { get; set; }
    public string Credit { get; set; }
    public string BalanceDebt { get; set; }
    public string BalanceCredit { get; set; }
}