using SiteManagement.Application.DueInformations.Queries.GetDueInformations;

namespace SiteManagement.MVC.Services;

public class DueSorterService : DataTableService<DueInformationDto>
{
    public DueSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<DueInformationDto> Searcher(IQueryable<DueInformationDto> value, string searchValue)
    {
        value = value.Where(m => 
            m.LeaseHolder.Contains(searchValue) || 
            m.Debt.Contains(searchValue) ||
            m.Credit.Contains(searchValue) ||
            m.BalanceDebt.Contains(searchValue) ||
            m.BalanceCredit.Contains(searchValue)
        );
        
        return value;
    }
}