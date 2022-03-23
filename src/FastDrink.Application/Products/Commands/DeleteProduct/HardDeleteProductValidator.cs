using FluentValidation;

namespace FastDrink.Application.Products.Commands.DeleteProduct;

public class HardDeleteProductValidator : AbstractValidator<HardDeleteProductCommand>
{
    public HardDeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
