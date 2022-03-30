using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Orders.DTOs;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<ResultOrderCreate>
{
    public int UserId { get; set; }

    public int AddressId { get; set; }

    public IList<OrderProductRequest> OrderProducts { get; set; } = null!;
}


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResultOrderCreate>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultOrderCreate> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var ids = request.OrderProducts.Select(x => x.ProductId).ToList();
        var products = await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);

        var order = new Order
        {
            AddressId = request.AddressId,
            TotalPrice = 0,
            UserId = request.UserId,
        };

        var orderEntry = await _dbContext.Order.AddAsync(order, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        Dictionary<string, string> errors = new();
        float totalPrice = 0;

        foreach (var product in products)
        {
            var productRequest = request.OrderProducts.FirstOrDefault(x => x.ProductId == product.Id);

            if (productRequest != null)
            {
                if ((product.Stock - productRequest.Quantity) < 0)
                {
                    errors.Add("No hay stock!", $"No hay stock suficiente para el pedido de {product.Name}.");
                }

                product.Stock -= productRequest.Quantity;

                var totalPriceProduct = CalculatePrice(product.Discount, product.Price, productRequest.Quantity);

                _dbContext.OrderProduct.Add(new OrderProduct
                {
                    OrderId = orderEntry.Entity.Id,
                    ProductId = product.Id,
                    Discount = product.Discount,
                    Quantity = productRequest.Quantity,
                    Price = totalPriceProduct
                });

                totalPrice += totalPriceProduct;
            }
        }

        if (errors.Count > 0)
        {
            _dbContext.Order.Remove(orderEntry.Entity);
            return ResultOrderCreate.Failure(errors);
        }

        orderEntry.Entity.TotalPrice = totalPrice;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ResultOrderCreate.Success(products);
    }

    private static float CalculatePrice(float? discount, float price, int quantity)
    {
        if (discount == null)
            return price * quantity;

        var discountPrice = price * discount / 100;
        return (price - discountPrice ?? 0) * quantity;
    }
}

public class ResultOrderCreate : Result
{
    public ResultOrderCreate(bool succeeded, IDictionary<string, string> errors, IList<Product> products) : base(succeeded, errors)
    {
        this.products = products;
    }

    public IList<Product> products { get; set; } = null!;

    public static ResultOrderCreate Success(IList<Product> products)
    {
        return new ResultOrderCreate(true, new Dictionary<string, string>(), products);
    }

    public static ResultOrderCreate Failure(IDictionary<string, string> errors)
    {
        return new ResultOrderCreate(false, errors, null);
    }
}