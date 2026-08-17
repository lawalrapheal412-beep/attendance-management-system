using System.Security.Cryptography;
using System.Text;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AttendanceManagementSystem.Application.Users.Commands.ResendPasswordSetup;

public sealed class ResendPasswordSetupCommandHandler
    : IRequestHandler<ResendPasswordSetupCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public ResendPasswordSetupCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<bool> Handle(
        ResendPasswordSetupCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return false;
        }

        // Don't generate setup links for users who already have a password.
        if (!string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            return false;
        }

        var rawToken = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        var tokenHash = Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(rawToken)));

        var expiresAt = DateTime.UtcNow.AddHours(24);

        user.SetPasswordSetupToken(
            tokenHash,
            expiresAt);

        await _userRepository.UpdateAsync(user);

        var frontendBaseUrl =
            _configuration["Frontend:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Frontend:BaseUrl is not configured.");

        var setupUrl =
            $"{frontendBaseUrl.TrimEnd('/')}/set-password?token={rawToken}";

        var htmlBody = $"""
            <h2>Set your Attendance Management System password</h2>
            <p>Hello {user.FullName},</p>
            <p>You requested a new password setup link.</p>
            <p>
                <a href="{setupUrl}">Create your password</a>
            </p>
            <p>This link expires in 24 hours.</p>
            <p>If you did not request this, you can ignore this email.</p>
            """;

        await _emailService.SendAsync(
            user.Email,
            "New password setup link",
            htmlBody,
            cancellationToken);

        return true;
    }
}