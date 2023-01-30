using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations;
using SiteManagement.MVC.Services.Misc;

namespace SiteManagement.MVC.Services.Register;

public class RegisterSorterService : DataTableService<RegisterInformationDto>
{
    public RegisterSorterService(IFormCollection formCollection)
        : base(formCollection)
    {
    }

    protected override IQueryable<RegisterInformationDto> Searcher
        (IQueryable<RegisterInformationDto> value, string searchValue)
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