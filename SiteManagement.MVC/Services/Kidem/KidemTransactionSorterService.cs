using SiteManagement.Application.KidemRelated.KidemTransactions.Queries;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Kidem;

public class KidemTransactionSorterService : DataTableService<KidemTransactionDto>
{
    public KidemTransactionSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<KidemTransactionDto> Searcher(IQueryable<KidemTransactionDto> value,
        string searchValue)
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