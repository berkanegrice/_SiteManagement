using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SiteManagement.Infrastructure.Services.CsvReaderHelper;

public static class Serializer<T>
{
    public static IEnumerable<T> Deserialize(byte[] bytes)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };
        
        using var ms = new MemoryStream(bytes);
        using var sr = new StreamReader(ms);
        using var csv = new CsvReader(sr, config);
        return csv.GetRecords<T>().ToList();
    }
}