using Chess.Domain.Common.Services;

namespace Chess.Infrastructure.Common.Services;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset CurrentOffsetDate() => DateTimeOffset.UtcNow;
    public DateTime CurrentDate() => DateTime.Now;
}