namespace AttendanceManagementSystem.Domain.Entities;

public class Department
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid FacultyId { get; private set; }
    public Faculty Faculty { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<Student> Students { get; private set; } = new List<Student>();
    public ICollection<Lecturer> Lecturers { get; private set; } = new List<Lecturer>();
    public ICollection<Course> Courses { get; private set; } = new List<Course>();

    private Department()
    {
    }

    public Department(
        string name,
        Guid facultyId)
    {
        Id = Guid.NewGuid();
        Name = name;
        FacultyId = facultyId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(
        string name,
        Guid facultyId)
    {
        Name = name;
        FacultyId = facultyId;
        UpdatedAt = DateTime.UtcNow;
    }
}