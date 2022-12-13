namespace SiteManagement.Application.Files.Queries.GetFiles;

public class FileOnDataBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FileType { get; set; }
    public string Extension { get; set; }
    public string Description { get; set; }
    public string UploadedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public byte[] Data { get; set; }
}