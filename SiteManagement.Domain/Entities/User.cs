using SiteManagement.Domain.Entities.RegisterRelated;

namespace SiteManagement.Domain.Entities;

public class User : BaseAuditableEntity
{
    public new int Id { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ICollection<RegisterInformation> RegisterInformations { get; set; }
}