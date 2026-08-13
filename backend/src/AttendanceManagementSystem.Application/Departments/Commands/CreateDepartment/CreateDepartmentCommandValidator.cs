using FluentValidation;

namespace AttendanceManagementSystem.Application.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentCommandValidator
    : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Department name is required.")
            .MaximumLength(150)
            .WithMessage("Department name must not exceed 150 characters.");

        RuleFor(x => x.FacultyId)
            .NotEmpty()
            .WithMessage("Faculty is required.");
    }
}