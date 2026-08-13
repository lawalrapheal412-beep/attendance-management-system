using AttendanceManagementSystem.Application.Semesters.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Queries.GetAllSemesters;

public sealed record GetAllSemestersQuery()
    : IRequest<IEnumerable<SemesterDto>>;