using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IAttendanceRecordRepository
{
    Task<AttendanceRecord?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<AttendanceRecord>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Guid> AddAsync(
        AttendanceRecord attendanceRecord,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        AttendanceRecord attendanceRecord,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}