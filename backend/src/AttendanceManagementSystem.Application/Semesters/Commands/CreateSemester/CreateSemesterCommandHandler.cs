using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.CreateSemester;

public sealed class CreateSemesterCommandHandler
    : IRequestHandler<CreateSemesterCommand, Guid>
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IAcademicSessionRepository _academicSessionRepository;

    public CreateSemesterCommandHandler(
        ISemesterRepository semesterRepository,
        IAcademicSessionRepository academicSessionRepository)
    {
        _semesterRepository = semesterRepository;
        _academicSessionRepository = academicSessionRepository;
    }

    public async Task<Guid> Handle(
        CreateSemesterCommand request,
        CancellationToken cancellationToken)
    {
        var academicSession =
            await _academicSessionRepository.GetByIdAsync(
                request.AcademicSessionId,
                cancellationToken);

        if (academicSession is null)
        {
            throw new InvalidOperationException(
                "The specified academic session does not exist.");
        }

        var semester = new Semester(
            request.Name,
            request.AcademicSessionId);

        await _semesterRepository.AddAsync(
            semester,
            cancellationToken);

        return semester.Id;
    }
}