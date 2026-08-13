using FluentValidation;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.UpdateLecturer;

public sealed class UpdateLecturerCommandValidator
    : AbstractValidator<UpdateLecturerCommand>
{
    public UpdateLecturerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Lecturer ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department is required.");
    }
}