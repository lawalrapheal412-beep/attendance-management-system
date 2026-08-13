using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IAttendanceSessionRepository
{
    Task<AttendanceSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<AttendanceSession>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        AttendanceSession attendanceSession,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        AttendanceSession attendanceSession,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}