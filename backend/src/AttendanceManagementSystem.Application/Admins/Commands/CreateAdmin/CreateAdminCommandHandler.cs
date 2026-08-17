using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Commands.CreateAdmin;

public sealed class CreateAdminCommandHandler
    : IRequestHandler<CreateAdminCommand, Guid>
{
    private readonly IAdminRepository _repository;
    private readonly IUserRepository _userRepository;

    public CreateAdminCommandHandler(
        IAdminRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(
        CreateAdminCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new InvalidOperationException(
                "The specified user does not exist.");
        }

        if (user.Role != UserRole.Admin)
        {
            throw new InvalidOperationException(
                "The specified user is not an Admin.");
        }

        var admin = new Admin(request.UserId);

        return await _repository.AddAsync(
            admin,
            cancellationToken);
    }
}