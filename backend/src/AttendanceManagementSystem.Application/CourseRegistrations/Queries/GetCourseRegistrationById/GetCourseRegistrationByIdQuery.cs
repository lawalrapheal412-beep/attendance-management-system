using AttendanceManagementSystem.Application.CourseRegistrations.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetCourseRegistrationById;

public sealed record GetCourseRegistrationByIdQuery(Guid Id)
    : IRequest<CourseRegistrationDto?>;