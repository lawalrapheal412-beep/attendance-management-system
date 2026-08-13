using FluentValidation;

namespace AttendanceManagementSystem.Application.Faculties.Commands.UpdateFaculty;

public sealed class UpdateFacultyCommandValidator
    : AbstractValidator<UpdateFacultyCommand>
{
    public UpdateFacultyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Faculty ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Faculty name is required.")
            .MaximumLength(150)
            .WithMessage("Faculty name must not exceed 150 characters.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Faculty code is required.")
            .MaximumLength(20)
            .WithMessage("Faculty code must not exceed 20 characters.");
    }
}