namespace SiteManagement.Application.DueRelated.DueTransactions.Queries.GetDueTransactions;

public class DueTransactionDto
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Detail { get; set; }
    
    public string Debt { get; set; }
    
    public string Credit { get; set; }
    
    public string BalanceDebt { get; set; }
    
    public string BalanceCredit { get; set; }
}