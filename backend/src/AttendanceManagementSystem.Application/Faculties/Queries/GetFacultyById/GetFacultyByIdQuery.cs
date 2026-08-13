using AttendanceManagementSystem.Application.Faculties.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Queries.GetFacultyById;

public sealed record GetFacultyByIdQuery(
    Guid Id) : IRequest<FacultyDto?>;