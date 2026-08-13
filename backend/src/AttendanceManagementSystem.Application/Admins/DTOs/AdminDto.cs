namespace AttendanceManagementSystem.Application.Admins.DTOs;

public sealed class AdminDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}