using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;
namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.UpdateCourseRegistration;

public sealed class UpdateCourseRegistrationCommandHandler
    : IRequestHandler<UpdateCourseRegistrationCommand, bool>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;

    public UpdateCourseRegistrationCommandHandler(
        ICourseRegistrationRepository courseRegistrationRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
    }

    public async Task<bool> Handle(
        UpdateCourseRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var courseRegistration = await _courseRegistrationRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (courseRegistration is null)
        {
            return false;
        }

        courseRegistration.Update(
            request.StudentId,
            request.CourseId,
            request.SemesterId,
            request.AcademicSessionId);

        return await _courseRegistrationRepository.UpdateAsync(
            courseRegistration,
            cancellationToken);
    }
}