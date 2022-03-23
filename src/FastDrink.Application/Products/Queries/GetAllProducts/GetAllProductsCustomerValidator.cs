using FluentValidation;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsCustomerValidator : AbstractValidator<GetAllProductsCustomerQuery>
{
    public GetAllProductsCustomerValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
