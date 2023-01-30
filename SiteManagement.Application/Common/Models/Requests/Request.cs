namespace SiteManagement.Application.Common.Models.Requests;

public abstract class Request
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Type { get; set; }
}