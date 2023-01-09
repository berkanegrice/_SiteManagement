using SiteManagement.Domain.Entities.DuesRelated;

namespace SiteManagement.Domain.Entities;

public class User : BaseAuditableEntity
{
    public new int Id { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int UserCode { get; set; }
    public DueInformation Due { get; set; }
}