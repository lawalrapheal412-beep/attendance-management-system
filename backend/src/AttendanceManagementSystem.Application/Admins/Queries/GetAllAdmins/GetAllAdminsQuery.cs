using AttendanceManagementSystem.Application.Admins.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Queries.GetAllAdmins;

public sealed record GetAllAdminsQuery
    : IRequest<IEnumerable<AdminDto>>;