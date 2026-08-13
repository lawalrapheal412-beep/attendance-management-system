using AttendanceManagementSystem.Domain.Enums;

using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FullName,
    string Email,
    string Password,
    UserRole Role
) : IRequest<Guid>;