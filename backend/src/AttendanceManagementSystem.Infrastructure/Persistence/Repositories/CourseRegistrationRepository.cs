using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class CourseRegistrationRepository : ICourseRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseRegistration?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.CourseRegistrations
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<CourseRegistration>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.CourseRegistrations
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        CourseRegistration courseRegistration,
        CancellationToken cancellationToken = default)
    {
        await _context.CourseRegistrations.AddAsync(
            courseRegistration,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return courseRegistration.Id;
    }

    public async Task<bool> UpdateAsync(
        CourseRegistration courseRegistration,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.CourseRegistrations
            .AnyAsync(x => x.Id == courseRegistration.Id, cancellationToken);

        if (!exists)
        {
            return false;
        }

        _context.CourseRegistrations.Update(courseRegistration);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var courseRegistration = await _context.CourseRegistrations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (courseRegistration == null)
        {
            return false;
        }

        _context.CourseRegistrations.Remove(courseRegistration);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}