namespace SiteManagement.Application.RegisterRelated.RegisterTransactions.Queries.GetDueTransactions;

public class DueDetailedInformationVm
{
    public IList<RegisterTransactionDto> Lists { get; init; } = new List<RegisterTransactionDto>();
}