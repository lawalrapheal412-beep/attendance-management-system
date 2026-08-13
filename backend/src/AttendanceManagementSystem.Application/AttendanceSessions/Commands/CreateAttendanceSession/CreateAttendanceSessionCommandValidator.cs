using FluentValidation;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.CreateAttendanceSession;

public sealed class CreateAttendanceSessionCommandValidator
    : AbstractValidator<CreateAttendanceSessionCommand>
{
    public CreateAttendanceSessionCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required.");

        RuleFor(x => x.LecturerId)
            .NotEmpty()
            .WithMessage("Lecturer ID is required.");

        RuleFor(x => x.SemesterId)
            .NotEmpty()
            .WithMessage("Semester ID is required.");

        RuleFor(x => x.AcademicSessionId)
            .NotEmpty()
            .WithMessage("Academic session ID is required.");

        RuleFor(x => x.SessionDate)
            .NotEmpty()
            .WithMessage("Session date is required.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be later than start time.");
    }
}