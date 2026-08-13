using FluentValidation;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.UpdateAcademicSession;

public sealed class UpdateAcademicSessionCommandValidator
    : AbstractValidator<UpdateAcademicSessionCommand>
{
    public UpdateAcademicSessionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Academic session ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Academic session name is required.")
            .MaximumLength(50)
            .WithMessage("Academic session name must not exceed 50 characters.");
    }
}