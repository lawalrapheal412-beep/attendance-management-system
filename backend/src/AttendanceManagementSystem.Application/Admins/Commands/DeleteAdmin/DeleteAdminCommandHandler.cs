using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Commands.DeleteAdmin;

public sealed class DeleteAdminCommandHandler
    : IRequestHandler<DeleteAdminCommand, bool>
{
    private readonly IAdminRepository _repository;

    public DeleteAdminCommandHandler(
        IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteAdminCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}