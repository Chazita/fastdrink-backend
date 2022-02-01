using FluentValidation;

namespace FastDrink.Application.Auth.Commands.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
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
