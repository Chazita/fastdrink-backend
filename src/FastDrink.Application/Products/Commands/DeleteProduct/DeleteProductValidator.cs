﻿using FluentValidation;

namespace FastDrink.Application.Products.Commands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .NotEmpty();
    }
}
