using FluentValidation;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.CreateLecturer;

public sealed class CreateLecturerCommandValidator
    : AbstractValidator<CreateLecturerCommand>
{
    public CreateLecturerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required.");
    }
}