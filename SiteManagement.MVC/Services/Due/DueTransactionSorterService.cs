using SiteManagement.Application.DueRelated.DueTransactions.Queries.GetDueTransactions;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Due;

public class DueTransactionSorterService : DataTableService<DueTransactionDto>
{
    public DueTransactionSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<DueTransactionDto> Searcher(IQueryable<DueTransactionDto> value, string searchValue)
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