using FluentValidation;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.UpdateAttendanceSessionStatus;

public sealed class UpdateAttendanceSessionStatusCommandValidator
    : AbstractValidator<UpdateAttendanceSessionStatusCommand>
{
    public UpdateAttendanceSessionStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Attendance session ID is required.");
    }
}