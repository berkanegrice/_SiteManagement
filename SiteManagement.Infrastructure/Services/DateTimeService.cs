using SiteManagement.Application.Common.Interfaces;

namespace SiteManagement.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}