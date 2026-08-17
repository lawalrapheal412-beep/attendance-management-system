using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using AttendanceManagementSystem.Domain.Entities;
using AttendanceManagementSystem.Domain.Enums;
using AttendanceManagementSystem.Infrastructure.Persistence;
using AttendanceManagementSystem.Infrastructure.Security;
using AttendanceManagementSystem.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AttendanceManagementSystem.IntegrationTests;

public sealed class AuthenticationTests
    : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;
    private readonly HttpClient _client;

    public AuthenticationTests(ApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidAdminCredentials_ReturnsOk()
    {
        var response = await LoginAsync(
            "admin@attendance.local",
            "ChangeThisPassword123!");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body =
            await response.Content.ReadFromJsonAsync<JsonElement>();

        Assert.Equal(
            "Admin",
            body.GetProperty("role").GetString());

        Assert.False(
            string.IsNullOrWhiteSpace(
                body.GetProperty("token").GetString()));
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
    {
        var response = await LoginAsync(
            "admin@attendance.local",
            "DefinitelyWrongPassword123!");

        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    [Fact]
    public async Task ProtectedUsersEndpoint_WithoutToken_ReturnsUnauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;

        var response = await _client.GetAsync("/api/users");

        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    [Fact]
    public async Task ProtectedUsersEndpoint_WithAdminToken_ReturnsOk()
    {
        var token = await GetTokenAsync(
            "admin@attendance.local",
            "ChangeThisPassword123!");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/api/users");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task ProtectedUsersEndpoint_WithStudentToken_ReturnsForbidden()
    {
        var (email, password) = await CreateTestStudentUserAsync();

        var token = await GetTokenAsync(email, password);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("/api/users");

        Assert.Equal(
            HttpStatusCode.Forbidden,
            response.StatusCode);
    }

    [Fact]
    public async Task SetPassword_WithInvalidToken_ReturnsBadRequest()
    {
        _client.DefaultRequestHeaders.Authorization = null;

        var request = new
        {
            token = "INVALID_SETUP_TOKEN",
            password = "AnotherStudent@2026!"
        };

        var response = await _client.PostAsJsonAsync(
            "/api/auth/set-password",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_WithExistingEmail_ReturnsConflict()
    {
        var token = await GetTokenAsync(
            "admin@attendance.local",
            "ChangeThisPassword123!");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new
        {
            fullName = "Duplicate Admin",
            email = "admin@attendance.local",
            role = 2
        };

        var response = await _client.PostAsJsonAsync(
            "/api/users",
            request);

        Assert.Equal(
            HttpStatusCode.Conflict,
            response.StatusCode);
    }

    private async Task<(string Email, string Password)>
        CreateTestStudentUserAsync()
    {
        var email =
            $"integration.student.{Guid.NewGuid():N}@example.com";

        var password = "StudentTest@2026!";

        using var scope = _factory.Services.CreateScope();

        var context =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var passwordHasher = new PasswordHasher();

        var user = User.Create(
            "Integration Test Student",
            email,
            passwordHasher.Hash(password),
            UserRole.Student);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return (email, password);
    }

    private async Task<HttpResponseMessage> LoginAsync(
        string email,
        string password)
    {
        _client.DefaultRequestHeaders.Authorization = null;

        return await _client.PostAsJsonAsync(
            "/api/auth/login",
            new
            {
                email,
                password
            });
    }

    private async Task<string> GetTokenAsync(
        string email,
        string password)
    {
        var response = await LoginAsync(email, password);

        response.EnsureSuccessStatusCode();

        var body =
            await response.Content.ReadFromJsonAsync<JsonElement>();

        return body
            .GetProperty("token")
            .GetString()
            ?? throw new InvalidOperationException(
                "Login response did not contain a token.");
    }
}