using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseRegistrations.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetCourseRegistrationById;

public sealed class GetCourseRegistrationByIdQueryHandler
    : IRequestHandler<GetCourseRegistrationByIdQuery, CourseRegistrationDto?>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;

    public GetCourseRegistrationByIdQueryHandler(
        ICourseRegistrationRepository courseRegistrationRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
    }

    public async Task<CourseRegistrationDto?> Handle(
        GetCourseRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _courseRegistrationRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (entity is null)
        {
            return null;
        }

        return new CourseRegistrationDto
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            CourseId = entity.CourseId,
            SemesterId = entity.SemesterId,
            AcademicSessionId = entity.AcademicSessionId,
            RegisteredAt = entity.RegisteredAt
        };
    }
}