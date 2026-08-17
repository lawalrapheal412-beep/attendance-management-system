using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Queries.GetStudentQrCode;

public sealed class GetStudentQrCodeQueryHandler
    : IRequestHandler<GetStudentQrCodeQuery, byte[]?>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IQrCodeService _qrCodeService;

    public GetStudentQrCodeQueryHandler(
        IStudentRepository studentRepository,
        IQrCodeService qrCodeService)
    {
        _studentRepository = studentRepository;
        _qrCodeService = qrCodeService;
    }

    public async Task<byte[]?> Handle(
        GetStudentQrCodeQuery request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository
            .GetByIdAsync(request.StudentId);

        if (student is null)
        {
            return null;
        }

        return _qrCodeService.GeneratePng(
            student.MatricNumber);
    }
}