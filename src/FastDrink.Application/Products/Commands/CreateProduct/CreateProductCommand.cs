﻿using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResult>
{
    public CreateProductRequest Product { get; set; }

    public string EmailCreator { get; set; }

}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;
    public CreateProductCommandHandler(IApplicationDbContext context, ICloudinaryService service)
    {
        _context = context;
        _cloudinaryService = service;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryExist = await _context.Category.FirstOrDefaultAsync(x => x.Id == request.Product.CategoryId, cancellationToken);
        var brandExist = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Product.CategoryId, cancellationToken);
        var containerExist = await _context.Container.FirstOrDefaultAsync(x => x.Id == request.Product.CategoryId, cancellationToken);

        if (categoryExist == null || brandExist == null || containerExist == null)
        {
            return CreateProductResult.Failure(new[]
            {
                $"The Category is {categoryExist}",
                $"The Brand is {brandExist}",
                $"The Container is {containerExist}",
            });
        }

        var product = new Product
        {
            Name = request.Product.Name,
            Price = request.Product.Price,
            Volumen = request.Product.Volumen,
            Stock = request.Product.Stock,
            Discount = null,
            CategoryId = request.Product.CategoryId,
            ContainerId = request.Product.ContainerId,
            BrandId = request.Product.BrandId,
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            LastModifiedBy = request.EmailCreator,
        };

        var entityEntry = await _context.Products.AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var imagesResult = await _cloudinaryService.UploadPhotos(request.Product.Photos);

        foreach (var imagen in imagesResult)
        {
            var productPhoto = new ProductPhoto
            {
                ProductId = entityEntry.Entity.Id,
                PhotoId = imagen.PublicId,
                PhotoUrl = imagen.SecureUrl.ToString(),
            };

            _context.ProductPhoto.Add(productPhoto);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return CreateProductResult.Success(entityEntry.Entity.Id, categoryExist);
    }
}

public class CreateProductResult : Result
{
    public CreateProductResult(bool succeeded, IEnumerable<string> errors, int productId, BaseType category) : base(succeeded, errors)
    {
        ProductId = productId;
        Category = category;
    }

    public int ProductId { get; set; }

    public BaseType? Category { get; set; }


    public static CreateProductResult Success(int productId, Category category)
    {
        return new CreateProductResult(true, Array.Empty<string>(), productId, category);
    }

    public static new CreateProductResult Failure(IEnumerable<string> errors)
    {
        return new CreateProductResult(false, errors, 0, null);
    }
}