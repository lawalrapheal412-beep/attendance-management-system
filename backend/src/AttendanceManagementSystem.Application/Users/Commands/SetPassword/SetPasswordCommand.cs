using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.SetPassword;

public sealed record SetPasswordCommand(
    string Token,
    string Password) : IRequest<bool>;