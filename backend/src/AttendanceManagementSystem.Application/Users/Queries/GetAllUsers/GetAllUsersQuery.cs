using AttendanceManagementSystem.Application.Users.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery()
    : IRequest<List<UserDto>>;