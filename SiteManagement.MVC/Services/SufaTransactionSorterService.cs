using SiteManagement.Application.SufaRelated.SufaTransactions.Queries;

namespace SiteManagement.MVC.Services;

public class SufaTransactionSorterService : DataTableService<SufaTransactionDto>
{
    public SufaTransactionSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<SufaTransactionDto> Searcher(IQueryable<SufaTransactionDto> value, string searchValue)
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