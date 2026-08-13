using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.CreateFaculty;

public sealed class CreateFacultyCommandHandler
    : IRequestHandler<CreateFacultyCommand, Guid>
{
    private readonly IFacultyRepository _repository;

    public CreateFacultyCommandHandler(
        IFacultyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var faculty = new Faculty(
            request.Name,
            request.Code);

        return await _repository.AddAsync(
            faculty,
            cancellationToken);
    }
}