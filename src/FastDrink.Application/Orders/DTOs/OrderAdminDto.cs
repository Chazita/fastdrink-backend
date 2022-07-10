using FastDrink.Application.Addresses.DTOs;
using FastDrink.Application.Auth.DTOs;
using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Orders.DTOs;

public class OrderAdminDto : IMapFrom<Order>
{
    public string Id { get; set; } = string.Empty;

    public float TotalPrice { get; set; }

    public UserDto User { get; set; } = null!;

    public string OrderStatus { get; set; } = "";

    public AddressDto Address { get; set; } = null!;

    public IList<OrderProductDto> Products { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? LastModified { get; set; }
}
