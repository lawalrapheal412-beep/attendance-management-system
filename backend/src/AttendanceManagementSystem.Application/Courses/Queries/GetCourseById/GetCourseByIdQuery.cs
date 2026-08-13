using MediatR;
using AttendanceManagementSystem.Application.Courses.DTOs;

namespace AttendanceManagementSystem.Application.Courses.Queries.GetCourseById;

public sealed record GetCourseByIdQuery(Guid Id)
    : IRequest<CourseDto?>;