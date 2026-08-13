using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Faculties.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Queries.GetAllFaculties;

public sealed class GetAllFacultiesQueryHandler
    : IRequestHandler<GetAllFacultiesQuery, IEnumerable<FacultyDto>>
{
    private readonly IFacultyRepository _repository;

    public GetAllFacultiesQueryHandler(
        IFacultyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FacultyDto>> Handle(
        GetAllFacultiesQuery request,
        CancellationToken cancellationToken)
    {
        var faculties = await _repository.GetAllAsync(
            cancellationToken);

        return faculties.Select(faculty =>
            new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                Code = faculty.Code,
                CreatedAt = faculty.CreatedAt,
                UpdatedAt = faculty.UpdatedAt
            });
    }
}