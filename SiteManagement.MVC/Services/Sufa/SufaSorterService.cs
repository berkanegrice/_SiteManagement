using SiteManagement.Application.SufaRelated.SufaInformations.Queries;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Sufa;

public class SufaSorterService : DataTableService<SufaInformationDto>
{
    public SufaSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<SufaInformationDto> Searcher
        (IQueryable<SufaInformationDto> value, string searchValue)
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