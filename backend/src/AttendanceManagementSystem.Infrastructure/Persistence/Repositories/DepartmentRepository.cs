using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Department>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        Department department,
        CancellationToken cancellationToken = default)
    {
        await _context.Departments.AddAsync(
            department,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return department.Id;
    }

    public async Task<bool> UpdateAsync(
        Department department,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.Departments
            .AnyAsync(x => x.Id == department.Id, cancellationToken);

        if (!exists)
        {
            return false;
        }

        _context.Departments.Update(department);

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