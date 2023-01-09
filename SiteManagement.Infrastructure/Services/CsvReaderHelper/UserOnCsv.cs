using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class UserOnCsv
{
    public string UserCode { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    [DefaultValue(" ")]
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}