namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public class UserModel : UserOnCsv
{
    private readonly UserOnCsv _userOnCsv;
    public UserModel(UserOnCsv userOnCsv)
    {
        _userOnCsv = userOnCsv;
    }
    public new string UserCode => _userOnCsv.UserCode[4..].Replace(" ", "").Trim();

    public new string UserName => _userOnCsv.UserName.Trim();

    public new string? PhoneNumber =>
        _userOnCsv.PhoneNumber?
            .Replace("(", "")
            .Replace(")", "")
            .Replace(" ", "")
            .Trim();

    public new string Email => _userOnCsv.Email;

    public new string Address => _userOnCsv.Address;
}