using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.RemoveLecturerFromCourse;

public sealed class RemoveLecturerFromCourseCommandHandler
    : IRequestHandler<RemoveLecturerFromCourseCommand, bool>
{
    private readonly ICourseLecturerRepository _repository;

    public RemoveLecturerFromCourseCommandHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        RemoveLecturerFromCourseCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.CourseId,
            request.LecturerId,
            cancellationToken);
    }
}