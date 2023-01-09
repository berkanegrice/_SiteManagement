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
        public const string View = "Dues.View";
        public const string Create = "Dues.Create";
        public const string Edit = "Dues.Edit";
        public const string Delete = "Dues.Delete";
    }
        
    public static class LeaseHolder
    {
        public const string View = "LeaseHolder.View";
        public const string Create = "LeaseHolder.Create";
        public const string Edit = "LeaseHolder.Edit";
        public const string Delete = "LeaseHolder.Delete";
    }
}