using AttendanceManagementSystem.Domain.Enums;

namespace AttendanceManagementSystem.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? PasswordSetupTokenHash { get; private set; }
    public DateTime? PasswordSetupTokenExpiresAt { get; private set; }


    public Student? Student { get; private set; }
    public Lecturer? Lecturer { get; private set; }
    public Admin? Admin { get; private set; }

    private User()
    {
    }

    public User(
        string fullName,
        string email,
        string passwordHash,
        UserRole role)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
    public static User Create(
        string fullName,
        string email,
        string passwordHash,
        UserRole role)
    {
        return new User(
            fullName,
            email,
            passwordHash,
            role);
    }
    public void Update(
        string fullName,
        string email,
        UserRole role,
        bool isActive)
    {
        FullName = fullName;
        Email = email;
        Role = role;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangePassword(string newPasswordHash)
{
    PasswordHash = newPasswordHash;
    PasswordSetupTokenHash = null;
    PasswordSetupTokenExpiresAt = null;
    UpdatedAt = DateTime.UtcNow;
}    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    public void SetPasswordSetupToken(
    string tokenHash,
    DateTime expiresAt)
{
    PasswordSetupTokenHash = tokenHash;
    PasswordSetupTokenExpiresAt = expiresAt;
}

    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        PasswordSetupTokenHash = null;
        PasswordSetupTokenExpiresAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

}