using AttendanceManagementSystem.Application.Admins.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Admins.Queries.GetAllAdmins;

public sealed class GetAllAdminsQueryHandler
    : IRequestHandler<GetAllAdminsQuery, IEnumerable<AdminDto>>
{
    private readonly IAdminRepository _repository;

    public GetAllAdminsQueryHandler(
        IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AdminDto>> Handle(
        GetAllAdminsQuery request,
        CancellationToken cancellationToken)
    {
        var admins = await _repository.GetAllAsync(
            cancellationToken);

        return admins.Select(admin =>
            new AdminDto
            {
                Id = admin.Id,
                UserId = admin.UserId,
                CreatedAt = admin.CreatedAt,
                UpdatedAt = admin.UpdatedAt
            });
    }
}