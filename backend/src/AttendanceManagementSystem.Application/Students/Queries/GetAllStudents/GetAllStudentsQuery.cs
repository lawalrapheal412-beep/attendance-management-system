using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Queries.GetAllStudents;

public record GetAllStudentsQuery()
    : IRequest<List<Student>>;