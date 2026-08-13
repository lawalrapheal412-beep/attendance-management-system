using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string FullName,
    string Email,
    UserRole Role,
    bool IsActive
) : IRequest<bool>;