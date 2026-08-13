namespace AttendanceManagementSystem.Application.AcademicSessions.DTOs;

public sealed class AcademicSessionDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public bool IsCurrent { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}