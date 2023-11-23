namespace Chess.Domain.Common.Services;

public interface IDateTimeService
{
    DateTimeOffset CurrentOffsetDate();
    DateTime CurrentDate();
}