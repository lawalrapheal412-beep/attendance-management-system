using FluentValidation;

namespace AttendanceManagementSystem.Application.Semesters.Commands.CreateSemester;

public sealed class CreateSemesterCommandValidator
    : AbstractValidator<CreateSemesterCommand>
{
    public CreateSemesterCommandValidator()
    {
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