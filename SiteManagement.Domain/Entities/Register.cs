using SiteManagement.Domain.Entities.Enums;

namespace SiteManagement.Domain.Entities;

public class Register
{
    public Register(IReadOnlyList<string> split)
    {
        RegisterName = split[0];
        RegisterType = split[1];
    }

    public string RegisterName { get; set; }
    public string RegisterType { get; set; }
}