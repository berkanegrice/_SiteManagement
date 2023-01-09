using SiteManagement.Application.Managements.Users.Queries.GetUserRoles;

namespace SiteManagement.MVC.Models;

public class ManageUserRolesVM
{
    public string UserId { get; set; }
    public List<UserRolesDto> UserRoles { get; set; }
}