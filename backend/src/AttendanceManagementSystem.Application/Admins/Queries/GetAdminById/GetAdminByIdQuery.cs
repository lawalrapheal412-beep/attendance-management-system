using AttendanceManagementSystem.Application.Admins.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Queries.GetAdminById;

public sealed record GetAdminByIdQuery(
    Guid Id) : IRequest<AdminDto?>;