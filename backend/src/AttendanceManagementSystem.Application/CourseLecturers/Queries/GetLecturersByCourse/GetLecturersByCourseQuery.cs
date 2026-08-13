using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetLecturersByCourse;

public sealed record GetLecturersByCourseQuery(
    Guid CourseId) : IRequest<IEnumerable<CourseLecturerDto>>;