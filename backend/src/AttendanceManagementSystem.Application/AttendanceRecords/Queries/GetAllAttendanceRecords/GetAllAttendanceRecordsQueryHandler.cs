using AttendanceManagementSystem.Application.AttendanceRecords.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAllAttendanceRecords;

public sealed class GetAllAttendanceRecordsQueryHandler
    : IRequestHandler<
        GetAllAttendanceRecordsQuery,
        IEnumerable<AttendanceRecordDto>>
{
    private readonly IAttendanceRecordRepository _repository;

    public GetAllAttendanceRecordsQueryHandler(
        IAttendanceRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AttendanceRecordDto>> Handle(
        GetAllAttendanceRecordsQuery request,
        CancellationToken cancellationToken)
    {
        var attendanceRecords = await _repository.GetAllAsync(
            cancellationToken);

        return attendanceRecords.Select(attendanceRecord =>
            new AttendanceRecordDto
            {
                Id = attendanceRecord.Id,
                AttendanceSessionId = attendanceRecord.AttendanceSessionId,
                StudentId = attendanceRecord.StudentId,
                Status = attendanceRecord.Status,
                MarkedAt = attendanceRecord.MarkedAt,
                CreatedAt = attendanceRecord.CreatedAt,
                UpdatedAt = attendanceRecord.UpdatedAt
            });
    }
}