using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.CreateAcademicSession;

public class CreateAcademicSessionCommandHandler
    : IRequestHandler<CreateAcademicSessionCommand, Guid>
{
    private readonly IAcademicSessionRepository _repository;

    public CreateAcademicSessionCommandHandler(
        IAcademicSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        var academicSession = new AcademicSession(
            request.Name,
            request.IsCurrent);

        return await _repository.AddAsync(
            academicSession,
            cancellationToken);
    }
}