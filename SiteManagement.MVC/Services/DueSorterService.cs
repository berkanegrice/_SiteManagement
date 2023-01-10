using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;

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
            m.Debt.Equals(searchValue) ||
            m.Credit.Equals(searchValue) ||
            m.BalanceDebt.Equals(searchValue) ||
            m.BalanceCredit.Equals(searchValue)
        );
        
        return value;
    }
}