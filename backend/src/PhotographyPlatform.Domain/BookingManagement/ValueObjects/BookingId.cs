using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.BookingManagement.ValueObjects;

public class BookingId : ValueObject
{
    public Guid Value { get; private set; }

    private BookingId(Guid value)
    {
        Value = value;
    }

    public static BookingId CreateUnique()
    {
        return new BookingId(Guid.NewGuid());
    }

    public static BookingId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Booking ID cannot be empty", nameof(value));

        return new BookingId(value);
    }

    public static BookingId Create()
    {
        return new BookingId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(BookingId bookingId)
    {
        return bookingId.Value;
    }
}