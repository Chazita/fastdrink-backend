using FluentValidation;

namespace FastDrink.Application.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .MinimumLength(1)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();

        RuleFor(x => x.Volumen)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.BrandId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();

        RuleFor(x => x.ContainerId)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();
    }
}
