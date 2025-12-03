using PhotographyPlatform.Domain.Common.Abstractions;

namespace PhotographyPlatform.Domain.Common.BaseTypes;

public abstract record DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTime OccurredOn { get; init; }
}