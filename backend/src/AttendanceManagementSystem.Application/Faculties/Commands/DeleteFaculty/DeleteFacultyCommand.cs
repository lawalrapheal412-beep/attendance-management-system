using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.DeleteFaculty;

public sealed record DeleteFacultyCommand(
    Guid Id) : IRequest<bool>;