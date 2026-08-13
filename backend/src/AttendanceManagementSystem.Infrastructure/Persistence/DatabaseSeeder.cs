using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using AttendanceManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AttendanceManagementSystem.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        // Check whether an Admin already exists
        var adminExists = await context.Admins.AnyAsync();

        if (adminExists)
        {
            return;
        }

        var email = configuration["InitialAdmin:Email"];
        var fullName = configuration["InitialAdmin:FullName"];
        var password = configuration["InitialAdmin:Password"];

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "Initial Admin configuration is missing.");
        }

        // Make sure the email isn't already being used
        var existingUser = await context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                $"A user with email '{email}' already exists, " +
                "but no Admin record exists for that user.");
        }

        // Hash the password using the existing BCrypt implementation
        var passwordHash = passwordHasher.Hash(password);

        // Create the User
        var user = User.Create(
            fullName,
            email,
            passwordHash,
            UserRole.Admin);

        // Create the corresponding Admin
        var admin = new Admin(user.Id);

        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            await context.Users.AddAsync(user);
            await context.Admins.AddAsync(admin);

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}