namespace SiteManagement.Application.DueRelated.DueTransactions.Queries.GetDueTransactions;

public class DueDetailedInformationVm
{
    public IList<DueTransactionDto> Lists { get; init; } = new List<DueTransactionDto>();
}