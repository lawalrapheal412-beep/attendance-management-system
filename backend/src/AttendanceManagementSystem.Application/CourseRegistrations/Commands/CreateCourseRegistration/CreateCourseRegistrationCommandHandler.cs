using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.CreateCourseRegistration;

public sealed class CreateCourseRegistrationCommandHandler
    : IRequestHandler<CreateCourseRegistrationCommand, Guid>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;

    public CreateCourseRegistrationCommandHandler(
        ICourseRegistrationRepository courseRegistrationRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
    }

    public async Task<Guid> Handle(
        CreateCourseRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var courseRegistration = new CourseRegistration(
            request.StudentId,
            request.CourseId,
            request.SemesterId,
            request.AcademicSessionId);

        return await _courseRegistrationRepository.AddAsync(
            courseRegistration,
            cancellationToken);
    }
}