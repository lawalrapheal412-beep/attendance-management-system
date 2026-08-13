namespace AttendanceManagementSystem.Domain.Entities;

public class Admin
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; } = null!;

    private Admin()
    {
    }

    public Admin(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
}