using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.CreateCourseRegistration;

public sealed record CreateCourseRegistrationCommand(
    Guid StudentId,
    Guid CourseId,
    Guid SemesterId,
    Guid AcademicSessionId)
    : IRequest<Guid>;