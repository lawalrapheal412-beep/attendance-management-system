using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCoursesByLecturer;

public sealed class GetCoursesByLecturerQueryHandler
    : IRequestHandler<
        GetCoursesByLecturerQuery,
        IEnumerable<CourseLecturerDto>>
{
    private readonly ICourseLecturerRepository _repository;

    public GetCoursesByLecturerQueryHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CourseLecturerDto>> Handle(
        GetCoursesByLecturerQuery request,
        CancellationToken cancellationToken)
    {
        var relationships = await _repository.GetByLecturerIdAsync(
            request.LecturerId,
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