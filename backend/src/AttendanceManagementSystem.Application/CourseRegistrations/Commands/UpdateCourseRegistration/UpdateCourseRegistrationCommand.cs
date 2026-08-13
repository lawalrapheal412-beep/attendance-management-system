using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.UpdateCourseRegistration;

public sealed record UpdateCourseRegistrationCommand(
    Guid Id,
    Guid StudentId,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicSessionId)
    : IRequest<bool>;