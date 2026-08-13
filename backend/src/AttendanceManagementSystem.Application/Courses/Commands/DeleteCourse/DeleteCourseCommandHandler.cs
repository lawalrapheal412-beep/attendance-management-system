using AttendanceManagementSystem.Application.Common.Interfaces;

using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler
    : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<bool> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (course is null)
        {
            return false;
        }

        return await _courseRepository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}