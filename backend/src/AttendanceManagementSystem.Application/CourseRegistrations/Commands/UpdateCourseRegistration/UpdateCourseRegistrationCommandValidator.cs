using FluentValidation;

namespace AttendanceManagementSystem.Application.CourseRegistrations.Commands.UpdateCourseRegistration;

public sealed class UpdateCourseRegistrationCommandValidator
    : AbstractValidator<UpdateCourseRegistrationCommand>
{
    public UpdateCourseRegistrationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Course registration ID is required.");

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