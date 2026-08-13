namespace AttendanceManagementSystem.Application.CourseLecturers.DTOs;

public sealed class CourseLecturerDto
{
    public Guid CourseId { get; init; }

    public Guid LecturerId { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}