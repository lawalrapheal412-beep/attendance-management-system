namespace AttendanceManagementSystem.Domain.Entities;

public class Faculty
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public ICollection<Department> Departments { get; private set; }
        = new List<Department>();

    private Faculty()
    {
    }

    public Faculty(
        string name,
        string code)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void Update(
        string name,
        string code)
    {
        Name = name;
        Code = code;
        UpdatedAt = DateTime.UtcNow;
    }
}