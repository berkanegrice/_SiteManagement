namespace SiteManagement.Domain.Entities.FileRelated;
public enum FileCategory
{
    DueInformation,
    DueTransaction,
    SufaInformation,
    SufaTransaction,
    KidemInformation,
    KidemTransaction,
}

public class FileOnDatabaseModel : FileModel
{
    public byte[] Data { get; set; }
    
    public FileCategory FileCategory { get; set; } 
}