using SiteManagement.Application.KidemRelated.KidemInformations.Queries;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Kidem;

public class KidemSorterService : DataTableService<KidemInformationDto>
{
    public KidemSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<KidemInformationDto> Searcher
        (IQueryable<KidemInformationDto> value, string searchValue)
    {
        value = value.Where(m =>
            m.LeaseHolder.Contains(searchValue) ||
            m.Debt.Equals(searchValue) ||
            m.Credit.Equals(searchValue) ||
            m.BalanceDebt.Equals(searchValue) ||
            m.BalanceCredit.Equals(searchValue)
        );

        return value;
    }
}