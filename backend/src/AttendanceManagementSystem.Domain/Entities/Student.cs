namespace AttendanceManagementSystem.Domain.Entities;

public class Student
{
    public Guid Id { get; private set; }
    public string MatricNumber { get; private set; } = string.Empty;
    public string FullName {get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
}