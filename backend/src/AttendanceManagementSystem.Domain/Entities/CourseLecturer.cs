namespace AttendanceManagementSystem.Domain.Entities;

public class CourseLecturer
{
    public Guid CourseId { get; private set; }

    public Guid LecturerId { get; private set; }

    public Course Course { get; private set; } = null!;

    public Lecturer Lecturer { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private CourseLecturer()
    {
    }

    public CourseLecturer(
        Guid courseId,
        Guid lecturerId)
    {
        CourseId = courseId;
        LecturerId = lecturerId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
}