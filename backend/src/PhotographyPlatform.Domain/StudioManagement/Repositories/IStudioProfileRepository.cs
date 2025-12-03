using ErrorOr;
using PhotographyPlatform.Domain.Authentication.ValueObjects;
using PhotographyPlatform.Domain.StudioManagement.Entities;
using PhotographyPlatform.Domain.StudioManagement.ValueObjects;

namespace PhotographyPlatform.Domain.StudioManagement.Repositories;

public interface IStudioProfileRepository
{
    Task<ErrorOr<StudioProfile>> GetByIdAsync(StudioProfileId id, CancellationToken cancellationToken = default);
    Task<ErrorOr<StudioProfile>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(StudioProfileId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudioProfile>> GetBySpecializationAsync(PhotographySpecialization specialization, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudioProfile>> GetByLocationAsync(string location, CancellationToken cancellationToken = default);
    Task<ErrorOr<Created>> CreateAsync(StudioProfile studioProfile, CancellationToken cancellationToken = default);
    Task<ErrorOr<Updated>> UpdateAsync(StudioProfile studioProfile, CancellationToken cancellationToken = default);
    Task<ErrorOr<Deleted>> DeleteAsync(StudioProfileId id, CancellationToken cancellationToken = default);
}