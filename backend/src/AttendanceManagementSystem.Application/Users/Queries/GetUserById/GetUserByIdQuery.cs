using AttendanceManagementSystem.Application.Users.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id)
    : IRequest<UserDto?>;