using System.Security.Cryptography;
using System.Text;
using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AttendanceManagementSystem.Application.Users.Commands.CreateUser;

public sealed class UserCommandHandler
    : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public UserCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<Guid> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        // Generate a cryptographically secure one-time token.
        var rawToken = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        // Store only the hash of the token.
        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(rawToken)));

        var expiresAt = DateTime.UtcNow.AddHours(24);

        // PasswordHash remains empty until the user sets a password.
        var user = User.Create(
            request.FullName,
            request.Email,
            string.Empty,
            request.Role);

        user.SetPasswordSetupToken(
            tokenHash,
            expiresAt);

        await _userRepository.AddAsync(user);

        var frontendBaseUrl =
            _configuration["Frontend:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Frontend:BaseUrl is not configured.");

        var setupUrl =
                $"{frontendBaseUrl.TrimEnd('/')}/set-password?token={rawToken}";
        var htmlBody = $"""
            <h2>Welcome to Attendance Management System</h2>
            <p>Hello {request.FullName},</p>
            <p>Your account has been created.</p>
            <p>Click the link below to create your password:</p>
            <p>
                <a href="{setupUrl}">Create your password</a>
            </p>
            <p>This link expires in 24 hours.</p>
            <p>If you did not expect this email, you can ignore it.</p>
            """;

        await _emailService.SendAsync(
            request.Email,
            "Create your Attendance Management System password",
            htmlBody,
            cancellationToken);

        return user.Id;
    }
}