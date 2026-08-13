using AttendanceManagementSystem.Domain.Enums;

namespace AttendanceManagementSystem.Application.AttendanceRecords.DTOs;

public sealed class AttendanceRecordDto
{
    public Guid Id { get; init; }

    public Guid AttendanceSessionId { get; init; }

    public Guid StudentId { get; init; }

    public AttendanceStatus Status { get; init; }

    public DateTime MarkedAt { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}