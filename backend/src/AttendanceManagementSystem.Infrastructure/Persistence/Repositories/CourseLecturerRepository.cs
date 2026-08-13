using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class CourseLecturerRepository : ICourseLecturerRepository
{
    private readonly ApplicationDbContext _context;

    public CourseLecturerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseLecturer?> GetByIdAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseLecturers
            .FirstOrDefaultAsync(
                x => x.CourseId == courseId &&
                     x.LecturerId == lecturerId,
                cancellationToken);
    }

    public async Task<IEnumerable<CourseLecturer>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.CourseLecturers
            .AsNoTracking()
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseLecturer>> GetByCourseIdAsync(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseLecturers
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseLecturer>> GetByLecturerIdAsync(
        Guid lecturerId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseLecturers
            .AsNoTracking()
            .Where(x => x.LecturerId == lecturerId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken)
    {
        return await _context.CourseLecturers
            .AnyAsync(
                x => x.CourseId == courseId &&
                     x.LecturerId == lecturerId,
                cancellationToken);
    }

    public async Task AddAsync(
        CourseLecturer courseLecturer,
        CancellationToken cancellationToken)
    {
        await _context.CourseLecturers.AddAsync(
            courseLecturer,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken)
    {
        var courseLecturer = await _context.CourseLecturers
            .FirstOrDefaultAsync(
                x => x.CourseId == courseId &&
                     x.LecturerId == lecturerId,
                cancellationToken);

        if (courseLecturer is null)
        {
            return false;
        }

        _context.CourseLecturers.Remove(courseLecturer);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}