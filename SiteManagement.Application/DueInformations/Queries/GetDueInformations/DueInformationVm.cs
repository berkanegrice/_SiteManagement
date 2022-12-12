using Microsoft.EntityFrameworkCore.Query.Internal;

namespace SiteManagement.Application.DueInformations.Queries.GetDueInformations;

public class DueInformationVm
{
    public IQueryable<DueInformationDto>? Lists { get; init; }
    
}