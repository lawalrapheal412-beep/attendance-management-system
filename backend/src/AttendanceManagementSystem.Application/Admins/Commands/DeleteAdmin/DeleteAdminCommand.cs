using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Commands.DeleteAdmin;

public sealed record DeleteAdminCommand(
    Guid Id) : IRequest<bool>;