using PhotographyPlatform.Domain.Common.BaseTypes;

namespace PhotographyPlatform.Domain.Authentication.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; private set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty", nameof(value));

        return new UserId(value);
    }

    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(UserId userId)
    {
        return userId.Value;
    }

    public static implicit operator UserId(Guid value)
    {
        return Create(value);
    }
}