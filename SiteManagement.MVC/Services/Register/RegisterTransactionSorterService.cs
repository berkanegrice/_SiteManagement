using SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Register;

public class RegisterTransactionSorterService : DataTableService<RegisterTransactionDto>
{
    public RegisterTransactionSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<RegisterTransactionDto> Searcher(IQueryable<RegisterTransactionDto> value, string searchValue)
    {
        value = value.Where(m => 
            // m.AccountCode.Contains(searchValue) ||
            m.Debt.Equals(searchValue) ||
            m.Credit.Equals(searchValue) ||
            m.BalanceDebt.Equals(searchValue) ||
            m.BalanceCredit.Equals(searchValue)
        );

        return value;
    }
}