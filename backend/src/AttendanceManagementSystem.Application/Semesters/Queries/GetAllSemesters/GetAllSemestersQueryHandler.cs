using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Semesters.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Queries.GetAllSemesters;

public sealed class GetAllSemestersQueryHandler
    : IRequestHandler<GetAllSemestersQuery, IEnumerable<SemesterDto>>
{
    private readonly ISemesterRepository _repository;

    public GetAllSemestersQueryHandler(
        ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SemesterDto>> Handle(
        GetAllSemestersQuery request,
        CancellationToken cancellationToken)
    {
        var semesters = await _repository.GetAllAsync(cancellationToken);

        return semesters.Select(s => new SemesterDto(
            s.Id,
            s.Name,
            s.AcademicSessionId));
    }
}