using FluentValidation;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsCustomerValidator : AbstractValidator<GetAllProductsCustomerQuery>
{
    public GetAllProductsCustomerValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);
    }
}
