using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.CreateAttendanceRecord;

public sealed class CreateAttendanceRecordCommandHandler
    : IRequestHandler<CreateAttendanceRecordCommand, Guid>
{
    private readonly IAttendanceRecordRepository _repository;
    private readonly IAttendanceSessionRepository _attendanceSessionRepository;
    private readonly IStudentRepository _studentRepository;

    public CreateAttendanceRecordCommandHandler(
        IAttendanceRecordRepository repository,
        IAttendanceSessionRepository attendanceSessionRepository,
        IStudentRepository studentRepository)
    {
        _repository = repository;
        _attendanceSessionRepository = attendanceSessionRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(
        CreateAttendanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        var attendanceSession =
            await _attendanceSessionRepository.GetByIdAsync(
                request.AttendanceSessionId,
                cancellationToken);

        if (attendanceSession is null)
        {
            throw new InvalidOperationException(
                "The specified attendance session does not exist.");
        }

        var student = await _studentRepository.GetByIdAsync(
            request.StudentId);

        if (student is null)
        {
            throw new InvalidOperationException(
                "The specified student does not exist.");
        }

        var attendanceRecord = new AttendanceRecord(
            request.AttendanceSessionId,
            request.StudentId,
            request.Status);

        return await _repository.AddAsync(
            attendanceRecord,
            cancellationToken);
    }
}