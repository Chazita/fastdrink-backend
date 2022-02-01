using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class ProductPhoto
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required, MaxLength(200)]
    public string PhotoUrl { get; set; }

    [Required, MaxLength(100)]
    public string PhotoId { get; set; }

}
