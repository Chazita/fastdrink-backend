using FluentValidation;

namespace FastDrink.Application.Products.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Product.Stock)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();

        RuleFor(x => x.Product.CategoryId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.Product.ContainerId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.Product.BrandId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.Product.Price)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();

        RuleFor(x => x.Product.Volumen)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.Product.Name)
            .MaximumLength(100)
            .MinimumLength(1)
            .NotEmpty();

        RuleFor(x => x.EmailCreator)
            .EmailAddress()
            .MaximumLength(150)
            .NotEmpty();
    }
}
