using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Lecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Queries.GetAllLecturers;

public sealed class GetAllLecturersQueryHandler
    : IRequestHandler<GetAllLecturersQuery, List<LecturerDto>>
{
    private readonly ILecturerRepository _lecturerRepository;

    public GetAllLecturersQueryHandler(
        ILecturerRepository lecturerRepository)
    {
        _lecturerRepository = lecturerRepository;
    }

    public async Task<List<LecturerDto>> Handle(
        GetAllLecturersQuery request,
        CancellationToken cancellationToken)
    {
        var lecturers = await _lecturerRepository.GetAllAsync();

        return lecturers
            .Select(x => new LecturerDto(
                x.Id,
                x.UserId,
                x.DepartmentId))
            .ToList();
    }
}