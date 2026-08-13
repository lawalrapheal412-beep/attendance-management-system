using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class AttendanceSessionRepository : IAttendanceSessionRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceSessionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.AttendanceSessions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<AttendanceSession>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.AttendanceSessions
            .AsNoTracking()
            .OrderByDescending(x => x.SessionDate)
            .ThenByDescending(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        AttendanceSession attendanceSession,
        CancellationToken cancellationToken)
    {
        await _context.AttendanceSessions.AddAsync(
            attendanceSession,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return attendanceSession.Id;
    }

    public async Task<bool> UpdateAsync(
        AttendanceSession attendanceSession,
        CancellationToken cancellationToken)
    {
        _context.AttendanceSessions.Update(attendanceSession);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var attendanceSession = await _context.AttendanceSessions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (attendanceSession is null)
        {
            return false;
        }

        _context.AttendanceSessions.Remove(attendanceSession);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}