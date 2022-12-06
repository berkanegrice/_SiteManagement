using SiteManagement.Application.DueInformations.Queries.GetDueInformations;

namespace SiteManagement.Application.Users.Queries.GetUsers;

public class UserDto
{
    public UserDto()
    {
        Users = new List<UserDto>();
    }
    
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int UserCode { get; set; }
    public DueInformationDto DueDto { get; set; }
    public IList<UserDto> Users { get; set; }
}