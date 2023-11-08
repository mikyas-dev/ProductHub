using FluentValidation;

namespace ProductHub.Application.Authentication.Commands.Roles;

public class RoleCommandValidator : AbstractValidator<RoleCommand>
{
    public RoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required");
    }
}