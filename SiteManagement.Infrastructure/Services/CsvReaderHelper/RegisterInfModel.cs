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

    public new int AccountCode { get; }
    public new double? Credit { get; }
    public new double? Debt { get; }
    public new double? BalanceDebt { get; }
    public new double? BalanceCredit { get; }
}