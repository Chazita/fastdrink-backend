using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands.RevocerProduct;

public class RecoverProductCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class RecoverProductCommandHandler : IRequestHandler<RecoverProductCommand, Result>
{
    private readonly IApplicationDbContext _context;
    public RecoverProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RecoverProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Producto", $"El product con el ID: {request.Id} no existe.");

            return Result.Failure(errors);
        }

        product.DeletedAt = null;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
