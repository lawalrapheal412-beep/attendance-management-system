using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface ISemesterRepository
{
    Task<Semester?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Semester>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        Semester semester,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Semester semester,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}