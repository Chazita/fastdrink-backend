using FluentValidation;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsAdminValidator : AbstractValidator<GetAllProductsAdminQuery>
{
    public GetAllProductsAdminValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);
    }
}
