using FluentValidation;

namespace AttendanceManagementSystem.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator
    : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(150)
            .WithMessage("Full name must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.")
            .MaximumLength(150)
            .WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid user role.");
    }
}