namespace AttendanceManagementSystem.Application.CourseRegistrations.DTOs;

public class CourseRegistrationDto
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public Guid CourseId { get; set; }

    public Guid SemesterId { get; set; }

    public Guid AcademicSessionId { get; set; }

    public DateTime RegisteredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}