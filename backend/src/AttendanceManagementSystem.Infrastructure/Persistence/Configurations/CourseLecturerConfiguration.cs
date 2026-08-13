using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class CourseLecturerConfiguration : IEntityTypeConfiguration<CourseLecturer>
{
    public void Configure(EntityTypeBuilder<CourseLecturer> builder)
    {
        builder.ToTable("CourseLecturers");

        builder.HasKey(cl => new
        {
            cl.CourseId,
            cl.LecturerId
        });

        builder.HasOne(cl => cl.Course)
            .WithMany(c => c.CourseLecturers)
            .HasForeignKey(cl => cl.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cl => cl.Lecturer)
            .WithMany(l => l.CourseLecturers)
            .HasForeignKey(cl => cl.LecturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(cl => cl.CreatedAt)
            .IsRequired();

        builder.Property(cl => cl.UpdatedAt);
    }
}