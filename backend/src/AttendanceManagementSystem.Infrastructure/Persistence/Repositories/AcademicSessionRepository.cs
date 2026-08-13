using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class AcademicSessionRepository : IAcademicSessionRepository
{
    private readonly ApplicationDbContext _context;

    public AcademicSessionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AcademicSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.AcademicSessions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<AcademicSession>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.AcademicSessions
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        AcademicSession academicSession,
        CancellationToken cancellationToken)
    {
        await _context.AcademicSessions.AddAsync(
            academicSession,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return academicSession.Id;
    }

    public async Task<bool> UpdateAsync(
        AcademicSession academicSession,
        CancellationToken cancellationToken)
    {
        _context.AcademicSessions.Update(academicSession);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var academicSession = await _context.AcademicSessions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (academicSession is null)
        {
            return false;
        }

        _context.AcademicSessions.Remove(academicSession);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}