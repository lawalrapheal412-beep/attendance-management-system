using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.CreateUser;

public class UserCommandHandler
    : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
      var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
        {
            throw new Exception("A user with this email already exists.");
        }

        var passwordHash =
            _passwordHasher.Hash(request.Password);

        var user = User.Create(
            request.FullName,
            request.Email,
            passwordHash,
            request.Role
        );

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}