using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
    public void Configure(EntityTypeBuilder<Semester> builder)
    {
        builder.ToTable("Semesters");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.UpdatedAt);

        builder.HasOne(s => s.AcademicSession)
            .WithMany(a => a.Semesters)
            .HasForeignKey(s => s.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.CourseRegistrations)
            .WithOne(cr => cr.Semester)
            .HasForeignKey(cr => cr.SemesterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<AttendanceSession>()
            .WithOne(a => a.Semester)
            .HasForeignKey(a => a.SemesterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}