using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class Address
{
    public int Id { get; set; }

    [Required]
    public string Province { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string Code { get; set; }

    public User? User { get; set; }

    public ICollection<OrderProduct>? Addresses { get; set; }
}
