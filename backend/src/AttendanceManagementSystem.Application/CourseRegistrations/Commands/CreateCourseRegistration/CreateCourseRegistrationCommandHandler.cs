using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.CreateCourseRegistration;

public sealed class CreateCourseRegistrationCommandHandler
    : IRequestHandler<CreateCourseRegistrationCommand, Guid>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IAcademicSessionRepository _academicSessionRepository;

    public CreateCourseRegistrationCommandHandler(
        ICourseRegistrationRepository courseRegistrationRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        ISemesterRepository semesterRepository,
        IAcademicSessionRepository academicSessionRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _semesterRepository = semesterRepository;
        _academicSessionRepository = academicSessionRepository;
    }

    public async Task<Guid> Handle(
        CreateCourseRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.StudentId);

        if (student is null)
        {
            throw new InvalidOperationException(
                "The specified student does not exist.");
        }

        var course = await _courseRepository.GetByIdAsync(
            request.CourseId,
            cancellationToken);

        if (course is null)
        {
            throw new InvalidOperationException(
                "The specified course does not exist.");
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