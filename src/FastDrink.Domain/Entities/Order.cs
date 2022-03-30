using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }

    [Required]
    public float TotalPrice { get; set; }

    [Required]
    public int AddressId { get; set; }

    public Address? Address { get; set; }

    public ICollection<OrderProduct>? Products { get; set; }
}
