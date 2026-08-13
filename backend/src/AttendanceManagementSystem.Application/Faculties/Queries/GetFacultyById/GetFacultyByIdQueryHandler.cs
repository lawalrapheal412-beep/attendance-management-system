using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Faculties.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Queries.GetFacultyById;

public sealed class GetFacultyByIdQueryHandler
    : IRequestHandler<GetFacultyByIdQuery, FacultyDto?>
{
    private readonly IFacultyRepository _repository;

    public GetFacultyByIdQueryHandler(
        IFacultyRepository repository)
    {
        _repository = repository;
    }

    public async Task<FacultyDto?> Handle(
        GetFacultyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var faculty = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (faculty is null)
        {
            return null;
        }

        return new FacultyDto
        {
            Id = faculty.Id,
            Name = faculty.Name,
            Code = faculty.Code,
            CreatedAt = faculty.CreatedAt,
            UpdatedAt = faculty.UpdatedAt
        };
    }
}