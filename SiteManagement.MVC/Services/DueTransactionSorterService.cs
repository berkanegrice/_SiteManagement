using SiteManagement.Application.DueTransactions.Queries.GetDueTransactions;

namespace SiteManagement.MVC.Services;

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
            m.Debt.Contains(searchValue) ||
            m.Credit.Contains(searchValue) ||
            m.BalanceDebt.Contains(searchValue) ||
            m.BalanceCredit.Contains(searchValue)
        );

        return value;
    }
}