using FluentValidation;

namespace AttendanceManagementSystem.Application.Semesters.Commands.UpdateSemester;

public sealed class UpdateSemesterCommandValidator
    : AbstractValidator<UpdateSemesterCommand>
{
    public UpdateSemesterCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Semester ID is required.");

        RuleFor(x => x.AcademicSessionId)
            .NotEmpty()
            .WithMessage("Academic session is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Semester name is required.")
            .MaximumLength(50)
            .WithMessage("Semester name must not exceed 50 characters.");
    }
}