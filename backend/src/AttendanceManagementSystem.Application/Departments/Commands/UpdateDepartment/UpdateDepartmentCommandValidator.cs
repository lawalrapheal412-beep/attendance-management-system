using FluentValidation;

namespace AttendanceManagementSystem.Application.Departments.Commands.UpdateDepartment;

public sealed class UpdateDepartmentCommandValidator
    : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Department ID is required.");

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