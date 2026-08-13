using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IFacultyRepository
{
    Task<Faculty?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Faculty>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        Faculty faculty,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Faculty faculty,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}