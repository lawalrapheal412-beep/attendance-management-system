using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.AcademicSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAcademicSessionById;

public class GetAcademicSessionByIdQueryHandler
    : IRequestHandler<GetAcademicSessionByIdQuery, AcademicSessionDto?>
{
    private readonly IAcademicSessionRepository _repository;

    public GetAcademicSessionByIdQueryHandler(
        IAcademicSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<AcademicSessionDto?> Handle(
        GetAcademicSessionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var academicSession = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (academicSession is null)
        {
            return null;
        }

        return new AcademicSessionDto
        {
            Id = academicSession.Id,
            Name = academicSession.Name,
            IsCurrent = academicSession.IsCurrent,
            CreatedAt = academicSession.CreatedAt,
            UpdatedAt = academicSession.UpdatedAt
        };
    }
}