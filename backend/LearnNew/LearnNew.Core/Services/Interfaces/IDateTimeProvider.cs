namespace LearnNew.Core.Services.Interfaces;
public interface IDateTimeProvider
{
    DateTime GetCurrent();
    DateTime GetCurrentUtc();
    DateOnly GetCurrentDateOnlyUtc();
    TimeOnly GetCurrentTimeOnlyUtc();
}
