using FluentValidation;

namespace Chess.Application.Identity.Commands.SignUp;

public sealed class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(250);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}