using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class AttendanceSessionConfiguration : IEntityTypeConfiguration<AttendanceSession>
{
    public void Configure(EntityTypeBuilder<AttendanceSession> builder)
    {
        builder.ToTable("AttendanceSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SessionDate)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.IsOpen)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lecturer)
            .WithMany()
            .HasForeignKey(x => x.LecturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Semester)
            .WithMany()
            .HasForeignKey(x => x.SemesterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AcademicSession)
            .WithMany(x => x.AttendanceSessions)
            .HasForeignKey(x => x.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.AttendanceRecords)
            .WithOne(x => x.AttendanceSession)
            .HasForeignKey(x => x.AttendanceSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}