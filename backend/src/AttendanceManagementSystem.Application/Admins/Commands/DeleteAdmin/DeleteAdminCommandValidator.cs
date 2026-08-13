using FluentValidation;

namespace AttendanceManagementSystem.Application.Admins.Commands.DeleteAdmin;

public sealed class DeleteAdminCommandValidator
    : AbstractValidator<DeleteAdminCommand>
{
    public DeleteAdminCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Admin ID is required.");
    }
}