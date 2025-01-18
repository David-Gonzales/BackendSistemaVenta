using Application.Interfaces;

namespace Shared.Services
{
    public class DateTimeServices : IDateTimeServices
    {
        public DateTime NowUTC => DateTime.UtcNow;
        public DateTime NowPeru => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
    }
}
