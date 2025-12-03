using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

public class PhotoStatus : ValueObject
{
    public static readonly PhotoStatus Unedited = new("Unedited");
    public static readonly PhotoStatus InProgress = new("InProgress");
    public static readonly PhotoStatus Edited = new("Edited");
    public static readonly PhotoStatus ClientApproved = new("ClientApproved");
    public static readonly PhotoStatus Rejected = new("Rejected");

    public string Value { get; private set; }

    private PhotoStatus(string value)
    {
        Value = value;
    }

    public static PhotoStatus FromString(string status)
    {
        return status switch
        {
            "Unedited" => Unedited,
            "InProgress" => InProgress,
            "Edited" => Edited,
            "ClientApproved" => ClientApproved,
            "Rejected" => Rejected,
            _ => throw new ArgumentException($"Invalid photo status: {status}")
        };
    }

    public bool CanBeEditedByPhotographer()
    {
        return this == Unedited || this == Rejected;
    }

    public bool CanBeApprovedByClient()
    {
        return this == Edited;
    }

    public bool IsCompleted()
    {
        return this == ClientApproved;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(PhotoStatus status)
    {
        return status.Value;
    }
}