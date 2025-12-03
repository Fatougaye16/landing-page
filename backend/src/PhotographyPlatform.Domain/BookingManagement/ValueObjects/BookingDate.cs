using ErrorOr;
using PhotographyPlatform.Domain.Common.BaseTypes;
using PhotographyPlatform.Domain.Common.Errors;

namespace PhotographyPlatform.Domain.BookingManagement.ValueObjects;

public class BookingDate : ValueObject
{
    public DateTime Value { get; private set; }

    private BookingDate(DateTime value)
    {
        Value = value;
    }

    public static ErrorOr<BookingDate> Create(DateTime date)
    {
        if (date.Date < DateTime.UtcNow.Date)
            return Errors.BookingManagement.BookingDateInPast;

        if (date.DayOfWeek == DayOfWeek.Sunday)
            return Errors.BookingManagement.BookingNotAvailableOnSundays;

        return new BookingDate(date.Date);
    }

    public bool IsWithinBusinessHours(TimeSpan time)
    {
        return time >= TimeSpan.FromHours(9) && time <= TimeSpan.FromHours(18);
    }

    public bool IsWeekend()
    {
        return Value.DayOfWeek == DayOfWeek.Saturday || Value.DayOfWeek == DayOfWeek.Sunday;
    }

    public bool IsAtLeast24HoursFromNow()
    {
        return Value.Subtract(DateTime.UtcNow).TotalHours >= 24;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString("yyyy-MM-dd");
    }
}