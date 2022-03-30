using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class Address
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Province { get; set; } = "";

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = "";

    [Required]
    [MaxLength(100)]
    public string Street { get; set; } = "";

    [Required]
    [MaxLength(4)]
    public string Code { get; set; } = "";

    public User? User { get; set; }
}
