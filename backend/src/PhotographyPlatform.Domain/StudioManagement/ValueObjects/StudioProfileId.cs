using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.StudioManagement.ValueObjects;

public class StudioProfileId : ValueObject
{
    public Guid Value { get; private set; }

    private StudioProfileId(Guid value)
    {
        Value = value;
    }

    public static StudioProfileId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Studio Profile ID cannot be empty", nameof(value));

        return new StudioProfileId(value);
    }

    public static StudioProfileId Create()
    {
        return new StudioProfileId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(StudioProfileId studioProfileId)
    {
        return studioProfileId.Value;
    }
}