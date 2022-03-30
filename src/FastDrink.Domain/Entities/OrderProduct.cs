using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class OrderProduct
{
    [Required]
    public int OrderId { get; set; }

    public Order? Order { get; set; }

    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required]
    [Range(0f, 100f)]
    public float? Discount { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public float Price { get; set; }
}
