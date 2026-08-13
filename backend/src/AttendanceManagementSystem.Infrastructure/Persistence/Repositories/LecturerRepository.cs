using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public sealed class LecturerRepository : ILecturerRepository
{
    private readonly ApplicationDbContext _context;

    public LecturerRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Lecturer lecturer)
    {
        await _context.Lecturers.AddAsync(lecturer);
        await _context.SaveChangesAsync();
    }

    public async Task<Lecturer?> GetByIdAsync(Guid id)
    {
        return await _context.Lecturers
            .Include(l => l.User)
            .Include(l => l.Department)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lecturer>> GetAllAsync()
    {
        return await _context.Lecturers
            .Include(l => l.User)
            .Include(l => l.Department)
            .ToListAsync();
    }

    public async Task UpdateAsync(Lecturer lecturer)
    {
        _context.Lecturers.Update(lecturer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Lecturer lecturer)
    {
        _context.Lecturers.Remove(lecturer);
        await _context.SaveChangesAsync();
    }
}