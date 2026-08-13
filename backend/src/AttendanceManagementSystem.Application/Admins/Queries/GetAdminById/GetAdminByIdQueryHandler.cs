using AttendanceManagementSystem.Application.Admins.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Queries.GetAdminById;

public sealed class GetAdminByIdQueryHandler
    : IRequestHandler<GetAdminByIdQuery, AdminDto?>
{
    private readonly IAdminRepository _repository;

    public GetAdminByIdQueryHandler(
        IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdminDto?> Handle(
        GetAdminByIdQuery request,
        CancellationToken cancellationToken)
    {
        var admin = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (admin is null)
        {
            return null;
        }

        return new AdminDto
        {
            Id = admin.Id,
            UserId = admin.UserId,
            CreatedAt = admin.CreatedAt,
            UpdatedAt = admin.UpdatedAt
        };
    }
}