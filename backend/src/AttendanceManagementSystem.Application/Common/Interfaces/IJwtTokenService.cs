using AttendanceManagementSystem.Domain.Entities;

namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}