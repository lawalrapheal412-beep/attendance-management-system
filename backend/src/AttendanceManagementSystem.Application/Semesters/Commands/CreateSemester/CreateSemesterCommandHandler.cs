using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.CreateSemester;

public class CreateSemesterCommandHandler
    : IRequestHandler<CreateSemesterCommand, Guid>
{
    private readonly ISemesterRepository _semesterRepository;

    public CreateSemesterCommandHandler(
        ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    public async Task<Guid> Handle(
        CreateSemesterCommand request,
        CancellationToken cancellationToken)
    {
        var semester = new Semester(
            request.Name,
            request.AcademicSessionId);

        await _semesterRepository.AddAsync(
            semester,
            cancellationToken);

        return semester.Id;
    }
}