using AttendanceManagementSystem.Application.Lecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Queries.GetAllLecturers;

public sealed record GetAllLecturersQuery
    : IRequest<List<LecturerDto>>;