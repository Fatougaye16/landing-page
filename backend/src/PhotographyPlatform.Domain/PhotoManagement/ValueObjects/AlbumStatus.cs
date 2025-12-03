using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

public class AlbumStatus : ValueObject
{
    public static readonly AlbumStatus Draft = new("Draft");
    public static readonly AlbumStatus ReadyForReview = new("ReadyForReview");
    public static readonly AlbumStatus InReview = new("InReview");
    public static readonly AlbumStatus Completed = new("Completed");
    public static readonly AlbumStatus Archived = new("Archived");

    public string Value { get; private set; }

    private AlbumStatus(string value)
    {
        Value = value;
    }

    public static AlbumStatus FromString(string status)
    {
        return status switch
        {
            "Draft" => Draft,
            "ReadyForReview" => ReadyForReview,
            "InReview" => InReview,
            "Completed" => Completed,
            "Archived" => Archived,
            _ => throw new ArgumentException($"Invalid album status: {status}")
        };
    }

    public bool CanAddPhotos()
    {
        return this == Draft;
    }

    public bool CanBeSubmittedForReview()
    {
        return this == Draft;
    }

    public bool CanBeReviewed()
    {
        return this == ReadyForReview;
    }

    public bool IsCompleted()
    {
        return this == Completed;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(AlbumStatus status)
    {
        return status.Value;
    }
}