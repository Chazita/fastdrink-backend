using Microsoft.AspNetCore.Http;

namespace FastDrink.Application.Products.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; }

    public float Price { get; set; }

    public float Volumen { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public int ContainerId { get; set; }

    public int BrandId { get; set; }

    public IList<IFormFile> Photos { get; set; }
}
