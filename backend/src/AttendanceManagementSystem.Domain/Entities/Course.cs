namespace AttendanceManagementSystem.Domain.Entities;

public class Course
{
    public Guid Id { get; private set; }

    public string Code { get; private set; } = string.Empty;

    public string Title { get; private set; } = string.Empty;

    public int Units { get; private set; }

    public Guid DepartmentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Department Department { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();
    public ICollection<CourseLecturer> CourseLecturers { get; private set; } = new List<CourseLecturer>();


    private Course()
    {
    }

    public Course(
        string code,
        string title,
        int units,
        Guid departmentId)
    {
        Id = Guid.NewGuid();

        Code = code;
        Title = title;
        Units = units;

        DepartmentId = departmentId;

        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(
        string code,
        string title,
        int units,
        Guid departmentId)
    {
        Code = code;
        Title = title;
        Units = units;

        DepartmentId = departmentId;

        UpdatedAt = DateTime.UtcNow;
    }
}