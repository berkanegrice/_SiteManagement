using SiteManagement.Application.Common.Mappings;

namespace SiteManagement.Application.DuesInformation.Queries.GetDuesInformation;

public class DuesInformationDto : IMapFrom<Domain.Entities.DuesRelated.DuesInformation>
{
    public DuesInformationDto()
    {
        DuesInformations = new List<DuesInformationDto>();
    }
    
    public int Id { get; set; }
    public string AccountCode { get; set; }
    public string Debt { get; set; }
    public string Credit { get; set; }
    public string BalanceDebt { get; set; }
    public string BalanceCredit { get; set; }
    public IList<DuesInformationDto> DuesInformations { get; set; }
}