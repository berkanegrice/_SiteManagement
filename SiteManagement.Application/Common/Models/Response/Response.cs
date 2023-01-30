namespace SiteManagement.Application.Common.Models.Response;

public abstract class Response
{
    public bool Status { get; set; }
    public int InsertedId { get; set; }
    public string Type { get; set; }
}