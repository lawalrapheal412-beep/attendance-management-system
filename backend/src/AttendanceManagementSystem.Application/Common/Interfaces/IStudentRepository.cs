using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id);
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByMatricNumberAsync(string matricNumber);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
}