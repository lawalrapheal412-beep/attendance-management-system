namespace AttendanceManagementSystem.Domain.Entities;

public class CourseRegistration
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid SemesterId { get; private set; }
    public Guid AcademicSessionId { get; private set; }
    public AcademicSession AcademicSession { get; private set; } = null!;
    public Student Student { get; private set; } = null!;
    public Course Course { get; private set; } = null!;
    public Semester Semester { get; private set; } = null!;
    public DateTime RegisteredAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private CourseRegistration()
    {
    }

    public CourseRegistration(
        Guid studentId,
        Guid courseId,
        Guid semesterId,
        Guid academicSessionId)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        CourseId = courseId;
        SemesterId = semesterId;
        AcademicSessionId = academicSessionId;
        RegisteredAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;

    }

    public void Update(
        Guid studentId,
        Guid courseId,
        Guid semesterId,
        Guid academicSessionId)
    {
        StudentId = studentId;
        CourseId = courseId;
        SemesterId = semesterId;
        AcademicSessionId = academicSessionId;
        UpdatedAt = DateTime.UtcNow;
    }
}