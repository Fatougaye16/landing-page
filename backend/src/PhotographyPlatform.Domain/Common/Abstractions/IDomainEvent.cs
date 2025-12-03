using MediatR;

namespace PhotographyPlatform.Domain.Common.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}