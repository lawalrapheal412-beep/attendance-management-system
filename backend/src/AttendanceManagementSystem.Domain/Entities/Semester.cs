namespace AttendanceManagementSystem.Domain.Entities;

public class Semester
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Guid AcademicSessionId { get; private set; }

    public AcademicSession AcademicSession { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();

    private Semester()
    {
    }

    public Semester(
        string name,
        Guid academicSessionId)
    {
        Id = Guid.NewGuid();
        Name = name;
        AcademicSessionId = academicSessionId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(
        string name,
        Guid academicSessionId)
    {
        Name = name;
        AcademicSessionId = academicSessionId;
        UpdatedAt = DateTime.UtcNow;
    }

}
