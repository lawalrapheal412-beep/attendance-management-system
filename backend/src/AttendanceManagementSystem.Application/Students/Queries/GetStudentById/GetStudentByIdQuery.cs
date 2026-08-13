using MediatR;
using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Students.Queries.GetStudentById;

public record GetStudentByIdQuery(
    Guid Id
) : IRequest<Student>;