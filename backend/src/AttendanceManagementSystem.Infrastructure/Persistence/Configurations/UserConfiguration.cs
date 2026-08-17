using AttendanceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AttendanceManagementSystem.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Role)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.PasswordSetupTokenHash)
            .HasMaxLength(128);

        builder.Property(x => x.PasswordSetupTokenExpiresAt);

        builder.HasOne(x => x.Student)
            .WithOne(x => x.User)
            .HasForeignKey<Student>(x => x.UserId);

        builder.HasOne(x => x.Lecturer)
            .WithOne(x => x.User)
            .HasForeignKey<Lecturer>(x => x.UserId);

        builder.HasOne(x => x.Admin)
            .WithOne(x => x.User)
            .HasForeignKey<Admin>(x => x.UserId);
    }
}