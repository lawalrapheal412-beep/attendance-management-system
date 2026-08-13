using AttendanceManagementSystem.Application.Authentication.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Authentication.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<LoginResponseDto?>;