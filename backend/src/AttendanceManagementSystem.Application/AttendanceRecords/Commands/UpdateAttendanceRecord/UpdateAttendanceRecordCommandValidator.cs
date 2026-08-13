using FluentValidation;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.UpdateAttendanceRecordStatus;

public sealed class UpdateAttendanceRecordStatusCommandValidator
    : AbstractValidator<UpdateAttendanceRecordStatusCommand>
{
    public UpdateAttendanceRecordStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Attendance record ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid attendance status.");
    }
}