using Microsoft.AspNetCore.Http;

namespace FastDrink.Application.Products.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;

    public float Price { get; set; }

    public float Volume { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public int ContainerId { get; set; }

    public int BrandId { get; set; }

    public IFormFile Photo { get; set; } = null!;
}
