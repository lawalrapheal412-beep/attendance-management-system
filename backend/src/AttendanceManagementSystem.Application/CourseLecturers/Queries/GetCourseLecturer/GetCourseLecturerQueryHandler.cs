using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCourseLecturer;

public sealed class GetCourseLecturerQueryHandler
    : IRequestHandler<GetCourseLecturerQuery, CourseLecturerDto?>
{
    private readonly ICourseLecturerRepository _repository;

    public GetCourseLecturerQueryHandler(
        ICourseLecturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseLecturerDto?> Handle(
        GetCourseLecturerQuery request,
        CancellationToken cancellationToken)
    {
        var courseLecturer = await _repository.GetByIdAsync(
            request.CourseId,
            request.LecturerId,
            cancellationToken);

        if (courseLecturer is null)
        {
            return null;
        }

        return new CourseLecturerDto
        {
            CourseId = courseLecturer.CourseId,
            LecturerId = courseLecturer.LecturerId,
            CreatedAt = courseLecturer.CreatedAt,
            UpdatedAt = courseLecturer.UpdatedAt
        };
    }
}