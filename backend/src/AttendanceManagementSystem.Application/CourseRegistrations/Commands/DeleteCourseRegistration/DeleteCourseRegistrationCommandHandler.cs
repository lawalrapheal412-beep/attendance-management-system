using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.DeleteCourseRegistration;

public sealed class DeleteCourseRegistrationCommandHandler
    : IRequestHandler<DeleteCourseRegistrationCommand, bool>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;

    public DeleteCourseRegistrationCommandHandler(
        ICourseRegistrationRepository courseRegistrationRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
    }

    public async Task<bool> Handle(
        DeleteCourseRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        return await _courseRegistrationRepository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}