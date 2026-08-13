using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCoursesByLecturer;

public sealed record GetCoursesByLecturerQuery(
    Guid LecturerId) : IRequest<IEnumerable<CourseLecturerDto>>;