using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Course?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<Course>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Course course,
        CancellationToken cancellationToken = default)
    {
        await _context.Courses.AddAsync(
            course,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(
        Course course,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.Courses
            .AnyAsync(x => x.Id == course.Id, cancellationToken);

        if (!exists)
        {
            return false;
        }

        _context.Courses.Update(course);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (course is null)
        {
            return false;
        }

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}