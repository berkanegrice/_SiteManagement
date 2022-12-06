namespace SiteManagement.Application.Constants;

public class ApplicationPermissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Due
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