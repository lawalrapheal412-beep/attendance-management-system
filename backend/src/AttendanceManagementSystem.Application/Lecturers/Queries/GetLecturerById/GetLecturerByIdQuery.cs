using AttendanceManagementSystem.Application.Lecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Queries.GetLecturerById;

public sealed record GetLecturerByIdQuery(
    Guid Id
) : IRequest<LecturerDto?>;