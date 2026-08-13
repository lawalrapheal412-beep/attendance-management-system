using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetLecturersByCourse;

public sealed class GetLecturersByCourseQueryHandler
    : IRequestHandler<
        GetLecturersByCourseQuery,
        IEnumerable<CourseLecturerDto>>
{
    private readonly ICourseLecturerRepository _repository;

    public GetLecturersByCourseQueryHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CourseLecturerDto>> Handle(
        GetLecturersByCourseQuery request,
        CancellationToken cancellationToken)
    {
        var relationships = await _repository.GetByCourseIdAsync(
            request.CourseId,
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