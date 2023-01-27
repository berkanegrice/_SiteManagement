namespace SiteManagement.Domain.Entities.RegisterRelated;

public class RegisterInformation : BaseAuditableEntity
{
    public new int Id { get; set; }
    public string? Debt { get; set; }
    public string? Credit { get; set; }
    public string? BalanceDebt { get; set; }
    public string? BalanceCredit { get; set; }
    
    public int AccountCode { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<RegisterTransaction> RegisterTransactions { get; set; }
}