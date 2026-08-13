using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IAcademicSessionRepository
{
    Task<AcademicSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<AcademicSession>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        AcademicSession academicSession,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        AcademicSession academicSession,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}