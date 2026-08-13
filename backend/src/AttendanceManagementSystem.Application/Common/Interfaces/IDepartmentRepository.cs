using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Department>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        Department department,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Department department,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}