using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Course>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Course course,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        Course course,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}