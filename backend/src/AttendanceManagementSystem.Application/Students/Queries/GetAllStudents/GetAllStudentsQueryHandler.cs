using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler
    : IRequestHandler<GetAllStudentsQuery, List<Student>>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsQueryHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<Student>> Handle(
        GetAllStudentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _studentRepository.GetAllAsync();
    }
}
