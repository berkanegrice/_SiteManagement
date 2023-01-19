namespace SiteManagement.Application;

public class RegisterModel
{
    public string RegisterName { get; set; }
    public string RegisterType { get; set; }
    
    public override string ToString()
    {
        return $"{RegisterName}" + " " + $"{RegisterType}";
    }
}