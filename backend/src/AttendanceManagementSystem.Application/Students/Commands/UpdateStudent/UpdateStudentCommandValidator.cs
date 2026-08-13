using FluentValidation;

namespace AttendanceManagementSystem.Application.Students.Commands.UpdateStudent;

public sealed class UpdateStudentCommandValidator
    : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Student ID is required.");

        RuleFor(x => x.MatricNumber)
            .NotEmpty()
            .WithMessage("Matric number is required.")
            .MaximumLength(30)
            .WithMessage("Matric number must not exceed 30 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Level)
            .IsInEnum()
            .WithMessage("Invalid student level.");
    }
}