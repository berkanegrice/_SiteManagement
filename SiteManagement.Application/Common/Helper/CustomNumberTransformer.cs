namespace SiteManagement.Application.Common.Helper;

public static class CustomNumberTransformer
{
    public static double ToDouble(this string? str)
    {
        if (double.TryParse(str, out var val))
        {
            return val;
        }
        str = str?.Replace('.', '-').Replace(',', '.').Replace('-', ',');
        return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str) ? double.Parse(str) : 0.0;
    }
}