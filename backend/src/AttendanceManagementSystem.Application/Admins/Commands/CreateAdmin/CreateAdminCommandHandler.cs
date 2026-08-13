using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Commands.CreateAdmin;

public sealed class CreateAdminCommandHandler
    : IRequestHandler<CreateAdminCommand, Guid>
{
    private readonly IAdminRepository _repository;

    public CreateAdminCommandHandler(
        IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateAdminCommand request,
        CancellationToken cancellationToken)
    {
        var admin = new Admin(request.UserId);

        return await _repository.AddAsync(
            admin,
            cancellationToken);
    }
}