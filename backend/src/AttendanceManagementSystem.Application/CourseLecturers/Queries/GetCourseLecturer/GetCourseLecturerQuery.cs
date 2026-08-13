using AttendanceManagementSystem.Application.CourseLecturers.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCourseLecturer;

public sealed record GetCourseLecturerQuery(
    Guid CourseId,
    Guid LecturerId) : IRequest<CourseLecturerDto?>;