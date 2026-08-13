namespace AttendanceManagementSystem.Domain.Entities;

public class AcademicSession
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public bool IsCurrent { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Semester> Semesters { get; private set; } = new List<Semester>();
    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();
    public ICollection<AttendanceSession> AttendanceSessions { get; private set; } = new List<AttendanceSession>();

    private AcademicSession()
    {
    }

    public AcademicSession(
        string name,
        bool isCurrent)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsCurrent = isCurrent;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(
    string name,
    bool isCurrent)
{
    Name = name;
    IsCurrent = isCurrent;
    UpdatedAt = DateTime.UtcNow;
}
}