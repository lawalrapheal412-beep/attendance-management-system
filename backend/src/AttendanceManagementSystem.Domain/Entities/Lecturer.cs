namespace AttendanceManagementSystem.Domain.Entities;

public class Lecturer
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User User { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public ICollection<CourseLecturer> CourseLecturers { get; private set; } = new List<CourseLecturer>();

    private Lecturer()
    {
    }

    public Lecturer(
        Guid userId,
        Guid departmentId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        DepartmentId = departmentId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Lecturer Create(
        Guid userId,
        Guid departmentId)
    {
        return new Lecturer(
            userId,
            departmentId);
    }

    public void Update(
        Guid userId,
        Guid departmentId)
    {
        UserId = userId;
        DepartmentId = departmentId;
        UpdatedAt = DateTime.UtcNow;
    }
}