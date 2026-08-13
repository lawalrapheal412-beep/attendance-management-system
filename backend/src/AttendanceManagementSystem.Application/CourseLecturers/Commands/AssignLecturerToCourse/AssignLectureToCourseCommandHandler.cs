using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.AssignLecturerToCourse;

public sealed class AssignLecturerToCourseCommandHandler
    : IRequestHandler<AssignLecturerToCourseCommand, bool>
{
    private readonly ICourseLecturerRepository _repository;

    public AssignLecturerToCourseCommandHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        AssignLecturerToCourseCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(
            request.CourseId,
            request.LecturerId,
            cancellationToken);

        if (exists)
        {
            return false;
        }

        var courseLecturer = new CourseLecturer(
            request.CourseId,
            request.LecturerId);

        await _repository.AddAsync(
            courseLecturer,
            cancellationToken);

        return true;
    }
}