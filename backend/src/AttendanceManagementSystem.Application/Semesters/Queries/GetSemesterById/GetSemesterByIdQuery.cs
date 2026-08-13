using AttendanceManagementSystem.Application.Semesters.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Queries.GetSemesterById;

public sealed record GetSemesterByIdQuery(Guid Id)
    : IRequest<SemesterDto?>;