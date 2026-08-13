using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.DeleteStudent;

public sealed record DeleteStudentCommand(Guid Id) : IRequest<bool>;