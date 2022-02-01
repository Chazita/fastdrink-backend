using FluentValidation;

namespace FastDrink.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(150)
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(24)
            .NotEmpty();
    }
}
