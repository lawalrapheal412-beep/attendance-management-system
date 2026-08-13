using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Commands.CreateAdmin;

public sealed record CreateAdminCommand(
    Guid UserId) : IRequest<Guid>;