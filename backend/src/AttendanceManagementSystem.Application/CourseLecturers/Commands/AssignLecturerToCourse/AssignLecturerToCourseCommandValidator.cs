using FluentValidation;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.AssignLecturerToCourse;

public sealed class AssignLecturerToCourseCommandValidator
    : AbstractValidator<AssignLecturerToCourseCommand>
{
    public AssignLecturerToCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required.");

        RuleFor(x => x.LecturerId)
            .NotEmpty()
            .WithMessage("Lecturer ID is required.");
    }
}