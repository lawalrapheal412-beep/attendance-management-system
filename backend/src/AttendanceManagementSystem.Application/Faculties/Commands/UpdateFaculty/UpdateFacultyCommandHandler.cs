using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Faculties.Commands.UpdateFaculty;

public sealed class UpdateFacultyCommandHandler
    : IRequestHandler<UpdateFacultyCommand, bool>
{
    private readonly IFacultyRepository _repository;

    public UpdateFacultyCommandHandler(
        IFacultyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var faculty = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (faculty is null)
        {
            return false;
        }

        faculty.Update(
            request.Name,
            request.Code);

        return await _repository.UpdateAsync(
            faculty,
            cancellationToken);
    }
}