using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.CourseRegistrations.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetAllCourseRegistrations;

public sealed class GetAllCourseRegistrationsQueryHandler
    : IRequestHandler<GetAllCourseRegistrationsQuery, IEnumerable<CourseRegistrationDto>>
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;

    public GetAllCourseRegistrationsQueryHandler(
        ICourseRegistrationRepository courseRegistrationRepository)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
    }

    public async Task<IEnumerable<CourseRegistrationDto>> Handle(
        GetAllCourseRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _courseRegistrationRepository.GetAllAsync(
            cancellationToken);

        return entities.Select(entity => new CourseRegistrationDto
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            CourseId = entity.CourseId,
            SemesterId = entity.SemesterId,
            AcademicSessionId = entity.AcademicSessionId,
            RegisteredAt = entity.RegisteredAt
        });
    }
}