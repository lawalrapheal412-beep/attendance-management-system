using Microsoft.OpenApi;
using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Infrastructure.Persistence;
using AttendanceManagementSystem.Application;
using AttendanceManagementSystem.Infrastructure;
using AttendanceManagementSystem.Api.Endpoints.Users;
using AttendanceManagementSystem.Api.Endpoints.Lecturers;
using AttendanceManagementSystem.Api.Endpoints.Students;
using AttendanceManagementSystem.Api.Endpoints.Courses;
using AttendanceManagementSystem.Api.Endpoints.CourseRegistrations;
using AttendanceManagementSystem.Api.Endpoints.Departments;
using AttendanceManagementSystem.Api.Endpoints.Semesters;
using AttendanceManagementSystem.Api.Endpoints.AcademicSessions;
using AttendanceManagementSystem.Api.Endpoints.AttendanceRecords;
using AttendanceManagementSystem.Api.Endpoints.AttendanceSessions;
using AttendanceManagementSystem.Api.Endpoints.Admins;
using AttendanceManagementSystem.Api.Endpoints.Faculties;
using AttendanceManagementSystem.Api.Endpoints.CourseLecturers;
using AttendanceManagementSystem.Api.ExceptionHandling;
using AttendanceManagementSystem.Api.Endpoints.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter your JWT bearer token."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<ConflictExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher>();

    await DatabaseSeeder.SeedAsync(
        context,
        passwordHasher,
        app.Configuration);
}

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Attendance Management System API v1");
});

}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapLecturerEndpoints();
app.MapStudentEndpoints();
app.MapUserEndpoints();
app.MapCourseEndpoints();
app.MapCourseRegistrationEndpoints();
app.MapDepartmentEndpoints();
app.MapSemesterEndpoints();
app.MapAcademicSessionEndpoints();
app.MapAttendanceRecordEndpoints();
app.MapAttendanceSessionEndpoints();
app.MapAdminEndpoints();
app.MapFacultyEndpoints();
app.MapCourseLecturerEndpoints();
app.MapAuthenticationEndpoints();
app.MapPasswordSetupEndpoints();

app.Run();

public partial class Program
{
}