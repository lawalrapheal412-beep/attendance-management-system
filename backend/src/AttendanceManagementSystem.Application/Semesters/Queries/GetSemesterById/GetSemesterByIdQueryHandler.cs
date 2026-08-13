using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Semesters.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Queries.GetSemesterById;

public sealed class GetSemesterByIdQueryHandler
    : IRequestHandler<GetSemesterByIdQuery, SemesterDto?>
{
    private readonly ISemesterRepository _repository;

    public GetSemesterByIdQueryHandler(
        ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<SemesterDto?> Handle(
        GetSemesterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var semester = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (semester is null)
        {
            return null;
        }

        return new SemesterDto(
            semester.Id,
            semester.Name,
            semester.AcademicSessionId);
    }
}