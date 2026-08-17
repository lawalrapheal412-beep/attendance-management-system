using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.ResendPasswordSetup;

public sealed record ResendPasswordSetupCommand(
    Guid UserId) : IRequest<bool>;