using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.RemoveLecturerFromCourse;

public sealed record RemoveLecturerFromCourseCommand(
    Guid CourseId,
    Guid LecturerId) : IRequest<bool>;