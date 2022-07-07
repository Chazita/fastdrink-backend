using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands.DeleteProduct;

public class HardDeleteProductCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class HardDeleteProductCommandHandler : IRequestHandler<HardDeleteProductCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public HardDeleteProductCommandHandler(IApplicationDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }


    public async Task<Result> Handle(HardDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Include(x => x.Photo).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
        {
            Dictionary<string, string> errors = new();

            if (product == null)
                errors.Add("Producto", "El producto no existe");

            return Result.Failure(errors);
        }


        if (product.Photo != null)
        {
            await _cloudinaryService.DeletePhoto(product.Photo.PhotoId);
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}