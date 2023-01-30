using CsvHelper.Configuration.Attributes;

namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class RegisterInfOnCsv
{
    [Index(0)]
    public string? AccountCode { get; set; }

    [Index(2)]
    public string? Debt { get; set; }
    
    [Index(3)]
    public string? Credit { get; set; }

    [Index(4)]
    public string? BalanceDebt { get; set; }

    [Index(5)]
    public string? BalanceCredit { get; set; }
    
    // public override string ToString()
    // {
    //     return
    //         $"insert into DueInformations(AccountCode,Debt,Credit,BalanceDebt,BalanceCredit) " +
    //         $"values ('{AccountCode}','{Debt.Trim()}','{Credit.Trim()}','{BalanceDebt.Trim()}','{BalanceCredit.Trim()}');";
    // }
}