namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class DueModel : DueOnCsv
{
    public DueModel(DueOnCsv dueOnCsv)
    {
        AccountCode = int.Parse(dueOnCsv.AccountCode!.Replace(" ", "").Trim());
        Credit = dueOnCsv.Credit!.Trim();
        Debt = dueOnCsv.Debt!.Trim();
        BalanceDebt = dueOnCsv.BalanceDebt!.Trim();
        BalanceCredit = dueOnCsv.BalanceCredit!.Trim();
    }

    public int AccountCode { get; }
    public string Credit { get; }
    public string Debt { get; }
    public string? BalanceDebt { get; }
    public string? BalanceCredit { get; }
}