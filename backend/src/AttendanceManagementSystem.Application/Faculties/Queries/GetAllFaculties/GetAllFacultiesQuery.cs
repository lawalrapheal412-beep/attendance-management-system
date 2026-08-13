using AttendanceManagementSystem.Application.Faculties.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Queries.GetAllFaculties;

public sealed record GetAllFacultiesQuery
    : IRequest<IEnumerable<FacultyDto>>;