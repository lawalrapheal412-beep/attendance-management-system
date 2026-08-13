namespace AttendanceManagementSystem.Application.Authentication.DTOs;

public sealed record LoginResponseDto(
    Guid UserId,
    string FullName,
    string Email,
    string Role,
    string Token);