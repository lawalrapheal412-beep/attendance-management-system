using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class CourseRegistrationConfiguration
    : IEntityTypeConfiguration<CourseRegistration>
{
    public void Configure(EntityTypeBuilder<CourseRegistration> builder)
    {
        builder.ToTable("CourseRegistrations");

        builder.HasKey(cr => cr.Id);

        builder.Property(cr => cr.RegisteredAt)
            .IsRequired();

        builder.Property(cr => cr.CreatedAt)
            .IsRequired();

        builder.Property(cr => cr.UpdatedAt);

        builder.HasOne(cr => cr.Student)
            .WithMany(s => s.CourseRegistrations)
            .HasForeignKey(cr => cr.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cr => cr.Course)
            .WithMany(c => c.CourseRegistrations)
            .HasForeignKey(cr => cr.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cr => cr.Semester)
            .WithMany(s => s.CourseRegistrations)
            .HasForeignKey(cr => cr.SemesterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cr => cr.AcademicSession)
            .WithMany(a => a.CourseRegistrations)
            .HasForeignKey(cr => cr.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(cr => new
        {
            cr.StudentId,
            cr.CourseId,
            cr.SemesterId,
            cr.AcademicSessionId
        }).IsUnique();
    }
}