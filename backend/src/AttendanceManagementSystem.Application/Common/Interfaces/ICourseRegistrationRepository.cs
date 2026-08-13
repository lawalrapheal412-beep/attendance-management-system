using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface ICourseRegistrationRepository
{
    Task<CourseRegistration?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<CourseRegistration>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        CourseRegistration courseRegistration,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        CourseRegistration courseRegistration,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}