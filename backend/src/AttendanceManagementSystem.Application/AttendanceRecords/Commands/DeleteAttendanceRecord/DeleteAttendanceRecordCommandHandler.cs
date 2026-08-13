using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.DeleteAttendanceRecord;

public sealed class DeleteAttendanceRecordCommandHandler
    : IRequestHandler<DeleteAttendanceRecordCommand, bool>
{
    private readonly IAttendanceRecordRepository _repository;

    public DeleteAttendanceRecordCommandHandler(
        IAttendanceRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteAttendanceRecordCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}