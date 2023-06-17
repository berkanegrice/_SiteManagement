
using CsvHelper.Configuration.Attributes;

namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class UserOnCsv
{
    [Index(0)]
    public string UserCode { get; set; }
    
    [Index(1)]
    public string UserName { get; set; }
    
    [Index(2)]
    public string Address { get; set; }
    
    [Index(3)]
    public string? PhoneNumber { get; set; }
    
    [Index(4)]
    public string Email { get; set; }
}