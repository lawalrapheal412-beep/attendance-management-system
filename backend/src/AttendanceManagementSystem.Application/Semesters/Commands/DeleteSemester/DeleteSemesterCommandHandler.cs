using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.DeleteSemester;

public sealed class DeleteSemesterCommandHandler
    : IRequestHandler<DeleteSemesterCommand, bool>
{
    private readonly ISemesterRepository _repository;

    public DeleteSemesterCommandHandler(
        ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteSemesterCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}