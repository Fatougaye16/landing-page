namespace PhotographyPlatform.Domain.Common.Abstractions;

public interface IAggregateRoot<out TId> : IEntity<TId>
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}