using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class AcademicSessionConfiguration : IEntityTypeConfiguration<AcademicSession>
{
    public void Configure(EntityTypeBuilder<AcademicSession> builder)
    {
        builder.ToTable("AcademicSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.IsCurrent)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Semesters)
            .WithOne(s => s.AcademicSession)
            .HasForeignKey(s => s.AcademicSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}