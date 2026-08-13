using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class SemesterRepository : ISemesterRepository
{
    private readonly ApplicationDbContext _context;

    public SemesterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Semester?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Semesters
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Semester>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Semesters
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        Semester semester,
        CancellationToken cancellationToken = default)
    {
        await _context.Semesters.AddAsync(
            semester,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return semester.Id;
    }

    public async Task<bool> UpdateAsync(
        Semester semester,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.Semesters
            .AnyAsync(x => x.Id == semester.Id, cancellationToken);

        if (!exists)
        {
            return false;
        }

        _context.Semesters.Update(semester);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var semester = await _context.Semesters
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (semester is null)
        {
            return false;
        }

        _context.Semesters.Remove(semester);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
