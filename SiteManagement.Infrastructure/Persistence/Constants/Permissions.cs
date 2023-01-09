namespace SiteManagement.Infrastructure.Persistence.Constants;

public static class Permissions
{
    public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}"
        };
    }
    
    public static class Dues
    {
        public const string View = "Permissions.Dues.View";
        public const string Create = "Permissions.Dues.Create";
        public const string Edit = "Permissions.Dues.Edit";
        public const string Delete = "Permissions.Dues.Delete";
    }
        
    public static class LeaseHolder
    {
        public const string View = "Permissions.LeaseHolder.View";
        public const string Create = "Permissions.LeaseHolder.Create";
        public const string Edit = "Permissions.LeaseHolder.Edit";
        public const string Delete = "Permissions.LeaseHolder.Delete";
    }
}