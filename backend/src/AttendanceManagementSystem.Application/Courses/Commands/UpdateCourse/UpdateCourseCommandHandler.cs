using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler
    : IRequestHandler<UpdateCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;

    public UpdateCourseCommandHandler(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<bool> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (course is null)
        {
            return false;
        }

        course.Update(
            request.Code,
            request.Title,
            request.Units,
            request.DepartmentId);

        return await _courseRepository.UpdateAsync(
            course,
            cancellationToken);
    }
}