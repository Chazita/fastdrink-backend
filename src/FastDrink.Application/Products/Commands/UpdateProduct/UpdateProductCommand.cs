using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Result>
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public float Price { get; set; }

    public float Volume { get; set; }

    public int Stock { get; set; }

    public float? Discount { get; set; }

    public int CategoryId { get; set; }

    public int ContainerId { get; set; }

    public int BrandId { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;

    public UpdateProductCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var intId = _hashids.Decode(request.Id)[0];
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == intId, cancellationToken);

        if (product == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Producto", $"El product con el ID: {request.Id} no existe.");

            return Result.Failure(errors);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Volume = request.Volume;
        product.Stock = request.Stock;
        product.Discount = request.Discount;
        product.CategoryId = request.CategoryId;
        product.ContainerId = request.ContainerId;
        product.BrandId = request.BrandId;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}