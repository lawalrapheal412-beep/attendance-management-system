namespace AttendanceManagementSystem.Application.Lecturers.DTOs;

public sealed record LecturerDto(
    Guid Id,
    Guid UserId,
    Guid DepartmentId
);