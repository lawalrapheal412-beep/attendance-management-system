using FluentValidation;

namespace AttendanceManagementSystem.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
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

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 character.");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid user role.");
    }
}