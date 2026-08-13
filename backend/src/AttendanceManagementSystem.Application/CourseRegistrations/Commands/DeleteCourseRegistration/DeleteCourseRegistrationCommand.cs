using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.DeleteCourseRegistration;

public sealed record DeleteCourseRegistrationCommand(Guid Id)
    : IRequest<bool>;