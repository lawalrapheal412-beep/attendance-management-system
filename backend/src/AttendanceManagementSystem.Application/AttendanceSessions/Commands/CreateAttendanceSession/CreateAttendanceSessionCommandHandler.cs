using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.CreateAttendanceSession;

public sealed class CreateAttendanceSessionCommandHandler
    : IRequestHandler<CreateAttendanceSessionCommand, Guid>
{
    private readonly IAttendanceSessionRepository _repository;
    private readonly ICourseRepository _courseRepository;
    private readonly ILecturerRepository _lecturerRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IAcademicSessionRepository _academicSessionRepository;

    public CreateAttendanceSessionCommandHandler(
        IAttendanceSessionRepository repository,
        ICourseRepository courseRepository,
        ILecturerRepository lecturerRepository,
        ISemesterRepository semesterRepository,
        IAcademicSessionRepository academicSessionRepository)
    {
        _repository = repository;
        _courseRepository = courseRepository;
        _lecturerRepository = lecturerRepository;
        _semesterRepository = semesterRepository;
        _academicSessionRepository = academicSessionRepository;
    }

    public async Task<Guid> Handle(
        CreateAttendanceSessionCommand request,
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

        var semester = await _semesterRepository.GetByIdAsync(
            request.SemesterId,
            cancellationToken);

        if (semester is null)
        {
            throw new InvalidOperationException(
                "The specified semester does not exist.");
        }

        var academicSession =
            await _academicSessionRepository.GetByIdAsync(
                request.AcademicSessionId,
                cancellationToken);

        if (academicSession is null)
        {
            throw new InvalidOperationException(
                "The specified academic session does not exist.");
        }

        var attendanceSession = new AttendanceSession(
            request.CourseId,
            request.LecturerId,
            request.SemesterId,
            request.AcademicSessionId,
            request.SessionDate,
            request.StartTime,
            request.EndTime);

        return await _repository.AddAsync(
            attendanceSession,
            cancellationToken);
    }
}