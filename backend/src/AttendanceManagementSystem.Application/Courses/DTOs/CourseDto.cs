namespace AttendanceManagementSystem.Application.Courses.DTOs;

public sealed record CourseDto(
    Guid Id,
    string Code,
    string Title,
    int Units,
    Guid DepartmentId);