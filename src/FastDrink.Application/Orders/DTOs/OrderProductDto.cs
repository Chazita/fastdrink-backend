using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Products.DTOs;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Orders.DTOs;

public class OrderProductDto : IMapFrom<OrderProduct>
{
    public ProductDto Product { get; set; } = new();

    public float? Discount { get; set; }

    public int Quantity { get; set; }

    public float Price { get; set; }
}
