using AttendanceManagementSystem.Application.AttendanceRecords.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAttendanceRecordById;

public sealed class GetAttendanceRecordByIdQueryHandler
    : IRequestHandler<GetAttendanceRecordByIdQuery, AttendanceRecordDto?>
{
    private readonly IAttendanceRecordRepository _repository;

    public GetAttendanceRecordByIdQueryHandler(
        IAttendanceRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<AttendanceRecordDto?> Handle(
        GetAttendanceRecordByIdQuery request,
        CancellationToken cancellationToken)
    {
        var attendanceRecord = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (attendanceRecord is null)
        {
            return null;
        }

        return new AttendanceRecordDto
        {
            Id = attendanceRecord.Id,
            AttendanceSessionId = attendanceRecord.AttendanceSessionId,
            StudentId = attendanceRecord.StudentId,
            Status = attendanceRecord.Status,
            MarkedAt = attendanceRecord.MarkedAt,
            CreatedAt = attendanceRecord.CreatedAt,
            UpdatedAt = attendanceRecord.UpdatedAt
        };
    }
}