using FluentValidation;

namespace AttendanceManagementSystem.Application.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandValidator
    : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.MatricNumber)
            .NotEmpty()
            .WithMessage("Matric number is required.")
            .MaximumLength(30)
            .WithMessage("Matric number must not exceed 30 characters.");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(150)
            .WithMessage("Full name must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.")
            .MaximumLength(150)
            .WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.Level)
            .IsInEnum()
            .WithMessage("Invalid student level.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.");
    }
}