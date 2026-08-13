using FluentValidation;

namespace AttendanceManagementSystem.Application.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandValidator
    : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Course ID is required.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Course code is required.")
            .MaximumLength(20)
            .WithMessage("Course code must not exceed 20 characters.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Course title is required.")
            .MaximumLength(150)
            .WithMessage("Course title must not exceed 150 characters.");

        RuleFor(x => x.Units)
            .InclusiveBetween(1, 10)
            .WithMessage("Course units must be between 1 and 10.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required.");
    }
}