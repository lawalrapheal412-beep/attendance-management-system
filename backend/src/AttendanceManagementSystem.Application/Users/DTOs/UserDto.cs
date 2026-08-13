using AttendanceManagementSystem.Domain.Enums;

namespace AttendanceManagementSystem.Application.Users.DTOs;

public sealed record UserDto(
    Guid Id,
    string FullName,
    string Email,
    UserRole Role,
    bool IsActive
);