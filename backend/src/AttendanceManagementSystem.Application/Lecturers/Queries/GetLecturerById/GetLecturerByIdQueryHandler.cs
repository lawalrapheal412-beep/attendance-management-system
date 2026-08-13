using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Lecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Queries.GetLecturerById;

public sealed class GetLecturerByIdQueryHandler
    : IRequestHandler<GetLecturerByIdQuery, LecturerDto?>
{
    private readonly ILecturerRepository _lecturerRepository;

    public GetLecturerByIdQueryHandler(
        ILecturerRepository lecturerRepository)
    {
        _lecturerRepository = lecturerRepository;
    }

    public async Task<LecturerDto?> Handle(
        GetLecturerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var lecturer = await _lecturerRepository.GetByIdAsync(request.Id);

        if (lecturer is null)
        {
            return null;
        }

        return new LecturerDto(
            lecturer.Id,
            lecturer.UserId,
            lecturer.DepartmentId);
    }
}