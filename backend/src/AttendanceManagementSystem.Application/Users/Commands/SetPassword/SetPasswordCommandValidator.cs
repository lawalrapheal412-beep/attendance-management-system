using FluentValidation;

namespace AttendanceManagementSystem.Application.Users.Commands.SetPassword;

public sealed class SetPasswordCommandValidator
    : AbstractValidator<SetPasswordCommand>
{
    public SetPasswordCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}