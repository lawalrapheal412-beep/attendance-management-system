namespace AttendanceManagementSystem.Application.Semesters.DTOs;

public sealed record SemesterDto(
    Guid Id,
    string Name,
    Guid AcademicSessionId
    );