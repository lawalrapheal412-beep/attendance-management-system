using Microsoft.EntityFrameworkCore;
using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<Lecturer> Lecturers => Set<Lecturer>();

    public DbSet<Student> Students => Set<Student>();

    public DbSet<Faculty> Faculties => Set<Faculty>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<CourseLecturer> CourseLecturers => Set<CourseLecturer>();

    public DbSet<AcademicSession> AcademicSessions => Set<AcademicSession>();

    public DbSet<Semester> Semesters => Set<Semester>();

    public DbSet<CourseRegistration> CourseRegistrations => Set<CourseRegistration>();

    public DbSet<AttendanceSession> AttendanceSessions => Set<AttendanceSession>();

    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}