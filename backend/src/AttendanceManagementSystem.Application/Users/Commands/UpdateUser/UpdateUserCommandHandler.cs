using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return false;
        }

        user.Update(
            request.FullName,
            request.Email,
            request.Role,
            request.IsActive);

        await _userRepository.UpdateAsync(user);

        return true;
    }
}