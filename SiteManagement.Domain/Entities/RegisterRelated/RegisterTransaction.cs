namespace SiteManagement.Domain.Entities.RegisterRelated;

public class RegisterTransaction : BaseAuditableEntity
{
    public new int Id { get; set; }
    public int AccountCode { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Detail { get; set; }
    
    public double? Debt { get; set; }
    
    public double? Credit { get; set; }
    
    public double? BalanceDebt { get; set; }
    
    public double? BalanceCredit { get; set; }
    
    public string Type { get; set; }
}