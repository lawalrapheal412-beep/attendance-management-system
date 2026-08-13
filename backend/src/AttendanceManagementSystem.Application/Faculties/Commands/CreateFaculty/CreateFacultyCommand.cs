using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.CreateFaculty;

public sealed record CreateFacultyCommand(
    string Name,
    string Code) : IRequest<Guid>;