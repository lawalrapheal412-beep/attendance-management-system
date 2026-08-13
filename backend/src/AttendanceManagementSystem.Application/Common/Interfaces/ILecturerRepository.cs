using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface ILecturerRepository
{
    Task<Lecturer?> GetByIdAsync(Guid id);

    Task<List<Lecturer>> GetAllAsync();

    Task AddAsync(Lecturer lecturer);

    Task UpdateAsync(Lecturer lecturer);

    Task DeleteAsync(Lecturer lecturer);
}