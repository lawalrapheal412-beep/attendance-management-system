using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetAllCourseLecturers;

public sealed class GetAllCourseLecturersQueryHandler
    : IRequestHandler<
        GetAllCourseLecturersQuery,
        IEnumerable<CourseLecturerDto>>
{
    private readonly ICourseLecturerRepository _repository;

    public GetAllCourseLecturersQueryHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CourseLecturerDto>> Handle(
        GetAllCourseLecturersQuery request,
        CancellationToken cancellationToken)
    {
        var relationships = await _repository.GetAllAsync(
            cancellationToken);

        return relationships.Select(x =>
            new CourseLecturerDto
            {
                CourseId = x.CourseId,
                LecturerId = x.LecturerId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });
    }
}