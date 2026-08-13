using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Users.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            return null;
        }

        return new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.Role,
            user.IsActive
        );
    }

}