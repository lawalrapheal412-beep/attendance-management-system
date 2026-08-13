using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Queries.GetStudentById;

public class GetStudentByIdQueryHandler
    : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student?> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _studentRepository.GetByIdAsync(request.Id);
    }
}