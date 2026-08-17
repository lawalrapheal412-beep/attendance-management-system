using AttendanceManagementSystem.Infrastructure.Email;
using AttendanceManagementSystem.Infrastructure.Security;
using AttendanceManagementSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AttendanceManagementSystem.Infrastructure.QR;

namespace AttendanceManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILecturerRepository, LecturerRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseRegistrationRepository, CourseRegistrationRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ISemesterRepository, SemesterRepository>();
        services.AddScoped<IAcademicSessionRepository, AcademicSessionRepository>();
        services.AddScoped<IAttendanceRecordRepository, AttendanceRecordRepository>();
        services.AddScoped<IAttendanceSessionRepository, AttendanceSessionRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ICourseLecturerRepository, CourseLecturerRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IQrCodeService, QrCodeService>();

        services.Configure<JwtSettings>(
    configuration.GetSection("Jwt"));

        var jwtSettings = configuration
            .GetSection("Jwt")
            .Get<JwtSettings>()
            ?? throw new InvalidOperationException(
                "JWT settings are not configured.");

        if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey))
        {
            throw new InvalidOperationException(
                "JWT SecretKey is not configured.");
        }

        if (Encoding.UTF8.GetByteCount(jwtSettings.SecretKey) < 32)
        {
            throw new InvalidOperationException(
                "JWT SecretKey must be at least 32 bytes.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.Configure<EmailSettings>(
            configuration.GetSection("Email"));

        services.AddScoped<IEmailService, EmailService>();

        return services;
     }
}