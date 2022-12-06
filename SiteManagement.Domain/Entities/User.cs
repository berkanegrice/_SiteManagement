using System.ComponentModel.DataAnnotations;
using SiteManagement.Domain.Entities.DuesRelated;

namespace SiteManagement.Domain.Entities;

public class User : BaseAuditableEntity
{
    public new int Id { get; set; }
    
    [MaxLength(70)]
    public string UserName { get; set; }
    
    public string Address { get; set; }

    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    [MaxLength(40)]
    public string Email { get; set; }
    public int UserCode { get; set; }
    public DueInformation Due { get; set; }
}