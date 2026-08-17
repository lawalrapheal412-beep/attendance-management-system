using MediatR;

namespace AttendanceManagementSystem.Application.Students.Queries.GetStudentQrCode;

public sealed record GetStudentQrCodeQuery(
    Guid StudentId) : IRequest<byte[]?>;