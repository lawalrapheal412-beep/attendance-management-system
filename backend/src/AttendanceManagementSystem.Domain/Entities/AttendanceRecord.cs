using AttendanceManagementSystem.Domain.Enums;

namespace AttendanceManagementSystem.Domain.Entities;

public class AttendanceRecord
{
    public Guid Id { get; private set; }

    public Guid AttendanceSessionId { get; private set; }

    public Guid StudentId { get; private set; }

    public AttendanceStatus Status { get; private set; }

    public DateTime MarkedAt { get; private set; }

    public AttendanceSession AttendanceSession { get; private set; } = null!;

    public Student Student { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private AttendanceRecord()
    {
    }

    public AttendanceRecord(
        Guid attendanceSessionId,
        Guid studentId,
        AttendanceStatus status)
    {
        Id = Guid.NewGuid();
        AttendanceSessionId = attendanceSessionId;
        StudentId = studentId;
        Status = status;
        MarkedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void MarkPresent()
    {
    Status = AttendanceStatus.Present;
    UpdatedAt = DateTime.UtcNow;
    }

    public void MarkLate()
    {
    Status = AttendanceStatus.Late;
    UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAbsent()
    {
    Status = AttendanceStatus.Absent;
    UpdatedAt = DateTime.UtcNow;
    }

}