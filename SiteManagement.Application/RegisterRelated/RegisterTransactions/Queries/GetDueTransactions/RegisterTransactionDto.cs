namespace SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;

public class RegisterTransactionDto
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Detail { get; set; }
    
    public double? Debt { get; set; }
    
    public double? Credit { get; set; }
    
    public double? BalanceDebt { get; set; }
    
    public double? BalanceCredit { get; set; }
}