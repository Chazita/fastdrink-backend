using FastDrink.Application.Addresses.Commands;

namespace FastDrink.Application.Orders.DTOs;

public class CreateOrderRequest
{
    public IList<OrderProductRequest> OrderProducts { get; set; } = null!;
    // Provisional property.
    public CreateAddressCommand Address { get; set; } = null!;
}
