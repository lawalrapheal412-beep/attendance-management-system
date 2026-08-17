using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.AssignLecturerToCourse;

public sealed class AssignLecturerToCourseCommandHandler
    : IRequestHandler<AssignLecturerToCourseCommand, bool>
{
    private readonly ICourseLecturerRepository _repository;
    private readonly ICourseRepository _courseRepository;
    private readonly ILecturerRepository _lecturerRepository;

    public AssignLecturerToCourseCommandHandler(
        ICourseLecturerRepository repository,
        ICourseRepository courseRepository,
        ILecturerRepository lecturerRepository)
    {
        _repository = repository;
        _courseRepository = courseRepository;
        _lecturerRepository = lecturerRepository;
    }

    public async Task<bool> Handle(
        AssignLecturerToCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(
            request.CourseId,
            cancellationToken);

        if (course is null)
        {
            throw new InvalidOperationException(
                "The specified course does not exist.");
        }

        var lecturer = await _lecturerRepository.GetByIdAsync(
            request.LecturerId);

        if (lecturer is null)
        {
            throw new InvalidOperationException(
                "The specified lecturer does not exist.");
        }

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