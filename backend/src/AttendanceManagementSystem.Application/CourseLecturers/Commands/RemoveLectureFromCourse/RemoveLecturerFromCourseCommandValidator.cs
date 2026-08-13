using FluentValidation;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.RemoveLecturerFromCourse;

public sealed class RemoveLecturerFromCourseCommandValidator
    : AbstractValidator<RemoveLecturerFromCourseCommand>
{
    public RemoveLecturerFromCourseCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required.");

        RuleFor(x => x.LecturerId)
            .NotEmpty()
            .WithMessage("Lecturer ID is required.");
    }
}