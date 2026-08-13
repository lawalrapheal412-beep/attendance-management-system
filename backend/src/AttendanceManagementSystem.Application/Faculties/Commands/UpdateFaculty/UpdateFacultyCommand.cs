using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.UpdateFaculty;

public sealed record UpdateFacultyCommand(
    Guid Id,
    string Name,
    string Code) : IRequest<bool>;