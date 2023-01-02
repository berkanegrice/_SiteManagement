using SiteManagement.Domain.Entities.FileRelated;

namespace SiteManagement.Domain.Events;

public class NewUserListAddedEvent : BaseEvent
{
    public NewUserListAddedEvent(FileOnDatabaseModel model)
    {
        Model = model;
    }
    
    public FileOnDatabaseModel Model { get; }
    
}