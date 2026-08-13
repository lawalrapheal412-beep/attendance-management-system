using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.Property(c => c.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Units)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.CourseRegistrations)
            .WithOne(cr => cr.Course)
            .HasForeignKey(cr => cr.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.CourseLecturers)
            .WithOne(cl => cl.Course)
            .HasForeignKey(cl => cl.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}