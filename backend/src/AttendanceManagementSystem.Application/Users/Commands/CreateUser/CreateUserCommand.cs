using AttendanceManagementSystem.Domain.Enums;

using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FullName,
    string Email,
    UserRole Role
) : IRequest<Guid>;