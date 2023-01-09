using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Infrastructure.Services.Misc;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}