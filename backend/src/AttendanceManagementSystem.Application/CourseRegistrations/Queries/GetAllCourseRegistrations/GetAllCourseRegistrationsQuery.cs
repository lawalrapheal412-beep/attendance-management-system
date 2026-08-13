using AttendanceManagementSystem.Application.CourseRegistrations.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetAllCourseRegistrations;

public sealed record GetAllCourseRegistrationsQuery
    : IRequest<IEnumerable<CourseRegistrationDto>>;