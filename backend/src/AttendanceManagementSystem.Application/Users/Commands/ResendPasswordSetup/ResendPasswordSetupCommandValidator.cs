using FluentValidation;

namespace AttendanceManagementSystem.Application.Users.Commands.ResendPasswordSetup;

public sealed class ResendPasswordSetupCommandValidator
    : AbstractValidator<ResendPasswordSetupCommand>
{
    public ResendPasswordSetupCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}