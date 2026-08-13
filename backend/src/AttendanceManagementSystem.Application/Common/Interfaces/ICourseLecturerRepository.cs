using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface ICourseLecturerRepository
{
    Task<CourseLecturer?> GetByIdAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken);

    Task<IEnumerable<CourseLecturer>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<CourseLecturer>> GetByCourseIdAsync(
        Guid courseId,
        CancellationToken cancellationToken);

    Task<IEnumerable<CourseLecturer>> GetByLecturerIdAsync(
        Guid lecturerId,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken);

    Task AddAsync(
        CourseLecturer courseLecturer,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid courseId,
        Guid lecturerId,
        CancellationToken cancellationToken);
}