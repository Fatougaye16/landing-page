using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.PhotoManagement.ValueObjects;

public class AlbumId : ValueObject
{
    public Guid Value { get; private set; }

    private AlbumId(Guid value)
    {
        Value = value;
    }

    public static AlbumId CreateUnique()
    {
        return new AlbumId(Guid.NewGuid());
    }

    public static AlbumId Create(Guid value)
    {
        return new AlbumId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(AlbumId albumId)
    {
        return albumId.Value;
    }
}