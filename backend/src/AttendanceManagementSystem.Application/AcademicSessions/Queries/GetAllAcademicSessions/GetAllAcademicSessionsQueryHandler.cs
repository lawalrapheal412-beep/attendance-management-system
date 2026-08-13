using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.AcademicSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAllAcademicSessions;

public class GetAllAcademicSessionsQueryHandler
    : IRequestHandler<GetAllAcademicSessionsQuery, IEnumerable<AcademicSessionDto>>
{
    private readonly IAcademicSessionRepository _repository;

    public GetAllAcademicSessionsQueryHandler(
        IAcademicSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AcademicSessionDto>> Handle(
        GetAllAcademicSessionsQuery request,
        CancellationToken cancellationToken)
    {
        var academicSessions = await _repository.GetAllAsync(
            cancellationToken);

        return academicSessions.Select(academicSession =>
            new AcademicSessionDto
            {
                Id = academicSession.Id,
                Name = academicSession.Name,
                IsCurrent = academicSession.IsCurrent,
                CreatedAt = academicSession.CreatedAt,
                UpdatedAt = academicSession.UpdatedAt
            });
    }
}