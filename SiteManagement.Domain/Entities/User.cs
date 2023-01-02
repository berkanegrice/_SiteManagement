using CsvHelper.Configuration.Attributes;
using SiteManagement.Domain.Entities.DuesRelated;

namespace SiteManagement.Domain.Entities;

public class User : BaseAuditableEntity
{
    public new int Id { get; set; }
    
    [Index(1)]
    public string UserName { get; set; }
    
    [Index(2)]
    public string Address { get; set; }
    
    [Index(3)]
    public string PhoneNumber { get; set; }

    [Index(4)]
    public string Email { get; set; }

    public int UserCode { get; set; }
    public DueInformation Due { get; set; }
}