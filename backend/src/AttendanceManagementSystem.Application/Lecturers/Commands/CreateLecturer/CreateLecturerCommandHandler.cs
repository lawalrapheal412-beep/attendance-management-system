using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.CreateLecturer;

public sealed class CreateLecturerCommandHandler
    : IRequestHandler<CreateLecturerCommand, Guid>
{
    private readonly ILecturerRepository _lecturerRepository;

    public CreateLecturerCommandHandler(
        ILecturerRepository lecturerRepository)
    {
        _lecturerRepository = lecturerRepository;
    }

    public async Task<Guid> Handle(
        CreateLecturerCommand request,
        CancellationToken cancellationToken)
    {
        var lecturer = Lecturer.Create(
            request.UserId,
            request.DepartmentId);

        await _lecturerRepository.AddAsync(lecturer);

        return lecturer.Id;
    }
}