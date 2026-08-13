using FluentValidation;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.CreateCourseRegistration;

public sealed class CreateCourseRegistrationCommandValidator
    : AbstractValidator<CreateCourseRegistrationCommand>
{
    public CreateCourseRegistrationCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("Student ID is required.");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required.");

        RuleFor(x => x.SemesterId)
            .NotEmpty()
            .WithMessage("Semester ID is required.");

        RuleFor(x => x.AcademicSessionId)
            .NotEmpty()
            .WithMessage("Academic session ID is required.");
    }
}