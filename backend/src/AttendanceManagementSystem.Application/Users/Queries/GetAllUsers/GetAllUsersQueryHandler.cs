using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Users.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(user => new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.Role,
            user.IsActive)).ToList();
    }
}