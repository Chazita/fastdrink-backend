using FluentValidation;

namespace FastDrink.Application.Auth.Commands.CreateAdmin;

public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(40)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MaximumLength(40)
            .NotEmpty();

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
