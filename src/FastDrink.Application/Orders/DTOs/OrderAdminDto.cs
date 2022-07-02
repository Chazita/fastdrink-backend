using FastDrink.Application.Auth.DTOs;
using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Orders.DTOs;

public class OrderAdminDto : IMapFrom<Order>
{
    public int Id { get; set; }

    public float TotalPrice { get; set; }

    public UserDto User { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public IList<OrderProductDto> Products { get; set; } = null!;
}
