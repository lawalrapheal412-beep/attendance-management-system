namespace AttendanceManagementSystem.Application.Departments.DTOs;

public sealed record DepartmentDto(
    Guid Id,
    string Name,
    Guid FacultyId
    );