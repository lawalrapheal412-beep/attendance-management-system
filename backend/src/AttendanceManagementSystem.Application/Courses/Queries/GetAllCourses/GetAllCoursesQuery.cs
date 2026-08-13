using AttendanceManagementSystem.Application.Courses.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Queries.GetAllCourses;

public sealed record GetAllCoursesQuery
    : IRequest<IEnumerable<CourseDto>>;