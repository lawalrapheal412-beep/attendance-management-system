using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id)
    : IRequest<bool>;