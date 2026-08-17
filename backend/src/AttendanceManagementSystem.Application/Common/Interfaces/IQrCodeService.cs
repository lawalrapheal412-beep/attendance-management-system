namespace AttendanceManagementSystem.Application.Common.Interfaces;

public interface IQrCodeService
{
    byte[] GeneratePng(string content);
}