using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Admin>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        Admin admin,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Admin admin,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}