using FluentValidation;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.CreateAcademicSession;

public sealed class CreateAcademicSessionCommandValidator
    : AbstractValidator<CreateAcademicSessionCommand>
{
    public CreateAcademicSessionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Academic session name is required.")
            .MaximumLength(50)
            .WithMessage("Academic session name must not exceed 50 characters.");
    }
}