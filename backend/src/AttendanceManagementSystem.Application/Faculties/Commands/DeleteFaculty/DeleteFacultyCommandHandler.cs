using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.DeleteFaculty;

public sealed class DeleteFacultyCommandHandler
    : IRequestHandler<DeleteFacultyCommand, bool>
{
    private readonly IFacultyRepository _repository;

    public DeleteFacultyCommandHandler(
        IFacultyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteFacultyCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}