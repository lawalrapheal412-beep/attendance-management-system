using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class AttendanceRecordRepository : IAttendanceRecordRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRecordRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceRecord?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.AttendanceRecords
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<AttendanceRecord>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.AttendanceRecords
            .AsNoTracking()
            .OrderByDescending(x => x.MarkedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        AttendanceRecord attendanceRecord,
        CancellationToken cancellationToken)
    {
        await _context.AttendanceRecords.AddAsync(
            attendanceRecord,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return attendanceRecord.Id;
    }

    public async Task<bool> UpdateAsync(
        AttendanceRecord attendanceRecord,
        CancellationToken cancellationToken)
    {
        _context.AttendanceRecords.Update(attendanceRecord);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var attendanceRecord = await _context.AttendanceRecords
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (attendanceRecord is null)
        {
            return false;
        }

        _context.AttendanceRecords.Remove(attendanceRecord);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}