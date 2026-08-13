using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class FacultyRepository : IFacultyRepository
{
    private readonly ApplicationDbContext _context;

    public FacultyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Faculty?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Faculties
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Faculty>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Faculties
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        Faculty faculty,
        CancellationToken cancellationToken)
    {
        await _context.Faculties.AddAsync(
            faculty,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return faculty.Id;
    }

    public async Task<bool> UpdateAsync(
        Faculty faculty,
        CancellationToken cancellationToken)
    {
        _context.Faculties.Update(faculty);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (faculty is null)
        {
            return false;
        }

        _context.Faculties.Remove(faculty);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}