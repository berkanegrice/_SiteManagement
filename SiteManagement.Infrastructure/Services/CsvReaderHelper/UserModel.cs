namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class UserModel : UserOnCsv
{
    private readonly UserOnCsv _userOnCsv;
    public UserModel(UserOnCsv record)
    {
        _userOnCsv = record;
    }
    // 13301001
    public new string UserCode
    {
        get => 
            _userOnCsv.UserCode[4..].Replace(" ", "").Trim();
    }

    public new string UserName
    {
        get => _userOnCsv.UserName.Trim();
    }
    public new string PhoneNumber
    {
        get => _userOnCsv.PhoneNumber
                .Replace("(", "")
                .Replace(")", "")
                .Replace(" ", "")
                .Trim();
    }
    public new string Email { get => _userOnCsv.Email;}
    
    public new string Address { get => _userOnCsv.Address;}
    
}