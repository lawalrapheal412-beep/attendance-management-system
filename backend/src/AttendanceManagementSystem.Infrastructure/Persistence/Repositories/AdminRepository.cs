using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;

    public AdminRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Admins
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Admin>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Admins
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(
        Admin admin,
        CancellationToken cancellationToken)
    {
        await _context.Admins.AddAsync(
            admin,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return admin.Id;
    }

    public async Task<bool> UpdateAsync(
        Admin admin,
        CancellationToken cancellationToken)
    {
        _context.Admins.Update(admin);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (admin is null)
        {
            return false;
        }

        _context.Admins.Remove(admin);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}