namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class UserModel : UserOnCsv
{
    private readonly UserOnCsv _userOnCsv;
    public UserModel(UserOnCsv record)
    {
        _userOnCsv = record;
    }
    
    public new int UserCode
    {
        get => int.Parse(
            _userOnCsv.UserCode.Replace(" ", "").Trim());
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