namespace SiteManagement.Application.Common.Helper;

public static class CustomNumberTransformer
{
    public static double ToDouble(this string? str)
    {
        return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str) ? double.Parse(str) : 0.0;
    }
}