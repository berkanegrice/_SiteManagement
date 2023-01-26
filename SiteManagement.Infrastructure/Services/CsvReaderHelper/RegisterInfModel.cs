namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class RegisterInfModel : RegisterInfOnCsv
{
    public RegisterInfModel(RegisterInfOnCsv registerInfOnCsv)
    {
        AccountCode = int.Parse(registerInfOnCsv.AccountCode!.Replace(" ", "").Trim());
        Credit = registerInfOnCsv.Credit!.Trim();
        Debt = registerInfOnCsv.Debt!.Trim();
        BalanceDebt = registerInfOnCsv.BalanceDebt!.Trim();
        BalanceCredit = registerInfOnCsv.BalanceCredit!.Trim();
    }

    public int AccountCode { get; }
    public string Credit { get; }
    public string Debt { get; }
    public string? BalanceDebt { get; }
    public string? BalanceCredit { get; }
}