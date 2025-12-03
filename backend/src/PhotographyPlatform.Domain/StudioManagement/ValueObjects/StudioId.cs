using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.StudioManagement.ValueObjects;

public class StudioId : ValueObject
{
    public Guid Value { get; private set; }

    private StudioId(Guid value)
    {
        Value = value;
    }

    public static StudioId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Studio ID cannot be empty", nameof(value));

        return new StudioId(value);
    }

    public static StudioId Create()
    {
        return new StudioId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(StudioId studioId)
    {
        return studioId.Value;
    }
}