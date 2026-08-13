using FluentValidation;

namespace AttendanceManagementSystem.Application.Admins.Commands.CreateAdmin;

public sealed class CreateAdminCommandValidator
    : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}