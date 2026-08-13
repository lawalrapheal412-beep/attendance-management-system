using AttendanceManagementSystem.Application.Authentication.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }
public async Task<LoginResponseDto?> Handle(
    LoginCommand request,
    CancellationToken cancellationToken)
{
    var user = await _userRepository
        .GetByEmailAsync(request.Email);

    if (user is null)
    {
        return null;
    }

    if (!user.IsActive)
    {
        return null;
    }

    var passwordIsValid = _passwordHasher.Verify(
        request.Password,
        user.PasswordHash);

    if (!passwordIsValid)
    {
        return null;
    }

    var token = _jwtTokenService.GenerateToken(user);

    return new LoginResponseDto(
        user.Id,
        user.FullName,
        user.Email,
        user.Role.ToString(),
        token);
}

}