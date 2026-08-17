using System.Security.Cryptography;
using System.Text;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Users.Commands.SetPassword;

public sealed class SetPasswordCommandHandler
    : IRequestHandler<SetPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public SetPasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(
        SetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var tokenHash = Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(request.Token)));

        var user = await _userRepository
            .GetByPasswordSetupTokenHashAsync(tokenHash);

        if (user is null)
        {
            return false;
        }

        if (user.PasswordSetupTokenExpiresAt is null ||
            user.PasswordSetupTokenExpiresAt <= DateTime.UtcNow)
        {
            return false;
        }

        var passwordHash = _passwordHasher.Hash(
            request.Password);

        user.SetPassword(passwordHash);

        await _userRepository.UpdateAsync(user);

        return true;
    }
}