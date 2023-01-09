namespace SiteManagement.Domain.Entities.DuesRelated;

public class DueInformation : BaseAuditableEntity
{
    public new int Id { get; set; }
    public string? Debt { get; set; }
    public string? Credit { get; set; }
    public string? BalanceDebt { get; set; }
    public string? BalanceCredit { get; set; }
    public int AccountCode { get; set; }
    public User User { get; set; }
    public ICollection<DueTransaction> Transactions { get; set; }
}