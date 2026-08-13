using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.CreateAttendanceRecord;

public sealed class CreateAttendanceRecordCommandHandler
    : IRequestHandler<CreateAttendanceRecordCommand, Guid>
{
    private readonly IAttendanceRecordRepository _repository;

    public CreateAttendanceRecordCommandHandler(
        IAttendanceRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateAttendanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        var attendanceRecord = new AttendanceRecord(
            request.AttendanceSessionId,
            request.StudentId,
            request.Status);

        return await _repository.AddAsync(
            attendanceRecord,
            cancellationToken);
    }
}