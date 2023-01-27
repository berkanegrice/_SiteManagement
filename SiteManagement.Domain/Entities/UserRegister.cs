using System.Reflection.Emit;
using SiteManagement.Domain.Entities.RegisterRelated;

namespace SiteManagement.Domain.Entities;

public class UserRegister
{
    public User User { get; set; }
    public int UserId { get; set; }
    
    public RegisterInformation RegisterInformation { get; set; }
    public int RegisterInformationId { get; set; }
}