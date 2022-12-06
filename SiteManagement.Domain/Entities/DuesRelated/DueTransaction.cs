namespace SiteManagement.Domain.Entities.DuesRelated;

public class DueTransaction : BaseAuditableEntity
{
    public new int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Detail { get; set; }
    
    public string Debt { get; set; }
    
    public string Credit { get; set; }
    
    public string BalanceDebt { get; set; }
    
    public string BalanceCredit { get; set; }
    
    public DueInformation DueInformation { get; set; }
}