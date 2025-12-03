using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.BookingManagement.ValueObjects;

public class BookingStatus : ValueObject
{
    public static readonly BookingStatus Pending = new("Pending");
    public static readonly BookingStatus Confirmed = new("Confirmed");
    public static readonly BookingStatus Declined = new("Declined");
    public static readonly BookingStatus Completed = new("Completed");
    public static readonly BookingStatus Cancelled = new("Cancelled");

    public string Value { get; private set; }

    private BookingStatus(string value)
    {
        Value = value;
    }

    public static BookingStatus Create(string status)
    {
        return status?.ToLowerInvariant() switch
        {
            "pending" => Pending,
            "confirmed" => Confirmed,
            "declined" => Declined,
            "completed" => Completed,
            "cancelled" => Cancelled,
            _ => throw new ArgumentException($"Invalid booking status: {status}")
        };
    }

    public static BookingStatus FromString(string status) => Create(status);

    public bool IsPending => this == Pending;
    public bool IsConfirmed => this == Confirmed;
    public bool IsDeclined => this == Declined;
    public bool IsCompleted => this == Completed;
    public bool IsCancelled => this == Cancelled;

    public bool CanBeConfirmed => IsPending;
    public bool CanBeDeclined => IsPending;
    public bool CanBeRescheduled => IsConfirmed;
    public bool CanBeCancelled => IsPending || IsConfirmed;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(BookingStatus status)
    {
        return status.Value;
    }
}