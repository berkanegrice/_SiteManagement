using SiteManagement.Application.Common.Helper;

namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class RegisterInfModel : RegisterInfOnCsv
{
    public RegisterInfModel(RegisterInfOnCsv registerInfOnCsv)
    {
        AccountCode = int.Parse(registerInfOnCsv.AccountCode!.Replace(" ", "").Trim());
        Credit = registerInfOnCsv.Credit!.Trim().ToDouble();
        Debt = registerInfOnCsv.Debt!.Trim().ToDouble();
        BalanceDebt = registerInfOnCsv.BalanceDebt!.Trim().ToDouble();
        BalanceCredit = registerInfOnCsv.BalanceCredit!.Trim().ToDouble();
    }

    public int AccountCode { get; }
    public double? Credit { get; }
    public double? Debt { get; }
    public double? BalanceDebt { get; }
    public double? BalanceCredit { get; }
}