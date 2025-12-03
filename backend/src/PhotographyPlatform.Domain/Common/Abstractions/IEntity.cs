namespace PhotographyPlatform.Domain.Common.Abstractions;

public interface IEntity<out TId>
{
    TId Id { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}