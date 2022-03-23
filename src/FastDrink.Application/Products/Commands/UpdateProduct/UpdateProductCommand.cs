using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Result>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public float Price { get; set; }

    public float Volumen { get; set; }

    public int Stock { get; set; }

    public float? Discount { get; set; }

    public int CategoryId { get; set; }

    public int ContainerId { get; set; }

    public int BrandId { get; set; }
}

public class UpdateProductComandHanlder : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductComandHanlder(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Producto", $"El product con el ID: {request.Id} no existe.");

            return Result.Failure(errors);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Volumen = request.Volumen;
        product.Stock = request.Stock;
        product.Discount = request.Discount;
        product.CategoryId = request.CategoryId;
        product.ContainerId = request.ContainerId;
        product.BrandId = request.BrandId;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}