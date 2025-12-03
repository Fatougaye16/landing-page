using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

public class PhotoId : ValueObject
{
    public Guid Value { get; private set; }

    private PhotoId(Guid value)
    {
        Value = value;
    }

    public static PhotoId CreateUnique()
    {
        return new PhotoId(Guid.NewGuid());
    }

    public static PhotoId Create(Guid value)
    {
        return new PhotoId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(PhotoId photoId)
    {
        return photoId.Value;
    }
}