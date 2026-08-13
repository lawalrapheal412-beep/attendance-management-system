using AttendanceManagementSystem.Domain.Enums;
namespace AttendanceManagementSystem.Domain.Entities;

public class Student
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public string MatricNumber { get; private set; } = string.Empty;
    public Level Level { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public User User { get; private set; } = null!;
    public Department Department { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<CourseRegistration> CourseRegistrations { get; private set; } = new List<CourseRegistration>();
    public ICollection<AttendanceRecord> AttendanceRecords { get; private set; } = new List<AttendanceRecord>();
    private Student()
    {
    }
    public Student(
       Guid userId,
       Guid departmentId,
       string matricNumber,
       Level level,
       DateOnly dateOfBirth)
    {
        Id = Guid.NewGuid();

        UserId = userId;
        DepartmentId = departmentId;

        MatricNumber = matricNumber;
        Level = level;
        DateOfBirth = dateOfBirth;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;

    }

    public static Student Create(
    Guid userId,
    Guid departmentId,
    string matricNumber,
    Level level,
    DateOnly dateOfBirth)
{
    return new Student(
        userId,
        departmentId,
        matricNumber,
        level,
        dateOfBirth);
}

    public void Update(
        string matricNumber,
        Guid userId,
        Guid departmentId,
        DateOnly dateOfBirth,
        Level level)
    {
        MatricNumber = matricNumber;
        UserId = userId;
        DepartmentId = departmentId;
        DateOfBirth = dateOfBirth;
        Level = (Level)level;

        UpdatedAt = DateTime.UtcNow;
    }
}