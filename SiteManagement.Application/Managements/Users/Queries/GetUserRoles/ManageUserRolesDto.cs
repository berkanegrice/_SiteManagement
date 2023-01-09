namespace SiteManagement.Application.Managements.Users.Queries.GetUserRoles;

public class ManageUserRolesDto
{
    public string UserId { get; set; }
    public List<UserRolesDto> UserRoles { get; set; }
}