using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.UpdateAttendanceRecordStatus;

public sealed class UpdateAttendanceRecordStatusCommandHandler
    : IRequestHandler<UpdateAttendanceRecordStatusCommand, bool>
{
    private readonly IAttendanceRecordRepository _repository;

    public UpdateAttendanceRecordStatusCommandHandler(
        IAttendanceRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateAttendanceRecordStatusCommand request,
        CancellationToken cancellationToken)
    {
        var attendanceRecord = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (attendanceRecord is null)
        {
            return false;
        }

        switch (request.Status)
        {
            case AttendanceStatus.Present:
                attendanceRecord.MarkPresent();
                break;

            case AttendanceStatus.Late:
                attendanceRecord.MarkLate();
                break;

            case AttendanceStatus.Absent:
                attendanceRecord.MarkAbsent();
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(request.Status),
                    request.Status,
                    "Invalid attendance status.");
        }

        return await _repository.UpdateAsync(
            attendanceRecord,
            cancellationToken);
    }
}