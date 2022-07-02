namespace FastDrink.Application.Orders.DTOs;

public class OrderProductRequest
{
    public string ProductId { get; set; } = "";

    public int Quantity { get; set; }
}
