using FluentValidation;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.CreateAttendanceRecord;

public sealed class CreateAttendanceRecordCommandValidator
    : AbstractValidator<CreateAttendanceRecordCommand>
{
    public CreateAttendanceRecordCommandValidator()
    {
        RuleFor(x => x.AttendanceSessionId)
            .NotEmpty()
            .WithMessage("Attendance session ID is required.");

        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("Student ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid attendance status.");
    }
}