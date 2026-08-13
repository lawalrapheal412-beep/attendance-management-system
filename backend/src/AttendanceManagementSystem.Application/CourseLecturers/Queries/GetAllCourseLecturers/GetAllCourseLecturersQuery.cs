using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetAllCourseLecturers;

public sealed record GetAllCourseLecturersQuery
    : IRequest<IEnumerable<CourseLecturerDto>>;